Imports System.Globalization
Imports System.Data
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System

Partial Public Class ArbeitscalcMain
    Inherits System.Web.UI.Page

    Protected Sub btnImportCSV_Click(sender As Object, e As EventArgs)
        lblStatus.Text = ""
        Try
            If Not FileUpload1.HasFile Then
                lblStatus.Text = "Bitte zuerst eine CSV-Datei auswählen!"
                Exit Sub
            End If

            Dim entries As New List(Of Tuple(Of DateTime, String, String, String))()
            Dim lastBemerkung As String = ""
            Using reader As New IO.StreamReader(FileUpload1.PostedFile.InputStream, System.Text.Encoding.UTF8)
                Dim headerRead As Boolean = False
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    If Not headerRead Then
                        headerRead = True
                        Continue While
                    End If
                    If String.IsNullOrWhiteSpace(line) Then Continue While

                    Dim columns = line.Split(";"c)
                    Dim isBuchung = False
                    Dim datumOk As Boolean = False
                    If columns.Length >= 2 Then
                        Dim dummyDate As DateTime
                        datumOk = DateTime.TryParseExact(columns(0).Trim(), "dd.MM.yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dummyDate)
                        If datumOk Then isBuchung = True
                    End If

                    If isBuchung And columns.Length >= 13 Then
                        Try
                            Dim datumUhrzeit = columns(0).Trim() & " " & columns(1).Trim()
                            Dim datum As DateTime = DateTime.ParseExact(datumUhrzeit, "dd.MM.yyyy HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                            Dim buchungstyp = columns(2).Trim().ToLower()
                            Dim meldungstext = columns(3).Trim().ToLower()
                            Dim baustelle = columns(8).Trim()
                            Dim bemerkung = columns(12).Trim()
                            Dim typ As String = ""
                            If buchungstyp = "fahrt" And meldungstext = "anfahrt" Then
                                typ = "Anfahrt"
                            ElseIf buchungstyp = "beginn" And meldungstext = "arbeitsbeginn" Then
                                typ = "Arbeitsbeginn"
                            ElseIf buchungstyp = "fahrt" And meldungstext = "abfahrt" Then
                                typ = "Abfahrt"
                            ElseIf buchungstyp = "ende" And meldungstext = "arbeitsende" Then
                                typ = "Arbeitsende"
                            End If
                            If typ <> "" Then
                                If String.IsNullOrWhiteSpace(bemerkung) AndAlso Not String.IsNullOrWhiteSpace(lastBemerkung) Then
                                    bemerkung = lastBemerkung
                                    lastBemerkung = ""
                                End If
                                entries.Add(Tuple.Create(datum, typ, baustelle, bemerkung))
                            End If
                        Catch ex As Exception
                            ' Fehler ignorieren
                        End Try
                    ElseIf columns.Length = 3 AndAlso Not datumOk Then
                        lastBemerkung = columns(0).Trim()
                    End If
                End While
            End Using

            ' Tagesblöcke wie in der Desktop-App bauen
            Dim sortedEntries = entries.OrderBy(Function(x) x.Item1).ToList()
            Dim tagesdaten As New DataTable()
            tagesdaten.Columns.Add("Tag")
            tagesdaten.Columns.Add("Datum")
            tagesdaten.Columns.Add("Baustelle")
            tagesdaten.Columns.Add("Bemerkung")
            tagesdaten.Columns.Add("Fahrzeit-Zeitraum")
            tagesdaten.Columns.Add("Arbeitszeit-Zeitraum")
            tagesdaten.Columns.Add("Pausenzeit (h)")
            tagesdaten.Columns.Add("Arbeitszeit (h)")
            tagesdaten.Columns.Add("Fahrzeit (h)")
            tagesdaten.Columns.Add("Überstunden (h)")
            tagesdaten.Columns.Add("Überstd. 25% (h)")
            tagesdaten.Columns.Add("Überstd. 50% (h)")
            tagesdaten.Columns.Add("Vergütete Arbeitszeit (h)")
            tagesdaten.Columns.Add("Datenintegrität")

            Dim blocks As New List(Of (Datum As Date, TagName As String, Baustelle As String, Bemerkung As String, Anfahrt As (Von As DateTime, Bis As DateTime)?, Arbeitszeit As (Von As DateTime, Bis As DateTime), Abfahrt As (Von As DateTime, Bis As DateTime)?))
            For i = 0 To sortedEntries.Count - 1
                If sortedEntries(i).Item2 = "Arbeitsbeginn" Then
                    Dim baustelle = sortedEntries(i).Item3
                    Dim bemerkung = sortedEntries(i).Item4
                    Dim datum = sortedEntries(i).Item1.Date
                    Dim arbeitsStart = sortedEntries(i).Item1

                    Dim anfahrt As (Von As DateTime, Bis As DateTime)? = Nothing
                    If i > 0 AndAlso sortedEntries(i - 1).Item2 = "Anfahrt" AndAlso sortedEntries(i - 1).Item3 = baustelle Then
                        anfahrt = (sortedEntries(i - 1).Item1, sortedEntries(i).Item1)
                    End If

                    Dim arbeitsEnde = arbeitsStart
                    Dim abfahrt As (Von As DateTime, Bis As DateTime)? = Nothing
                    Dim j = i + 1
                    While j < sortedEntries.Count
                        If sortedEntries(j).Item2 = "Abfahrt" AndAlso sortedEntries(j).Item3 = baustelle Then
                            arbeitsEnde = sortedEntries(j).Item1
                            If j + 1 < sortedEntries.Count AndAlso sortedEntries(j + 1).Item2 = "Arbeitsende" AndAlso sortedEntries(j + 1).Item3 = baustelle Then
                                abfahrt = (sortedEntries(j).Item1, sortedEntries(j + 1).Item1)
                            Else
                                Dim abfahrtBis As DateTime = sortedEntries(j).Item1
                                For k = j + 1 To sortedEntries.Count - 1
                                    If sortedEntries(k).Item2 = "Arbeitsbeginn" Then
                                        abfahrtBis = sortedEntries(k).Item1
                                        Exit For
                                    End If
                                Next
                                abfahrt = (sortedEntries(j).Item1, abfahrtBis)
                            End If
                            Exit While
                        ElseIf sortedEntries(j).Item2 = "Arbeitsbeginn" Then
                            arbeitsEnde = sortedEntries(j).Item1
                            Exit While
                        ElseIf sortedEntries(j).Item2 = "Arbeitsende" AndAlso sortedEntries(j).Item3 = baustelle Then
                            arbeitsEnde = sortedEntries(j).Item1
                            Exit While
                        End If
                        j += 1
                    End While

                    blocks.Add((datum, datum.ToString("dddd", New Globalization.CultureInfo("de-DE")), baustelle, bemerkung, anfahrt, (arbeitsStart, arbeitsEnde), abfahrt))
                End If
            Next

            Dim bemerkungenProTag As New Dictionary(Of Date, String)
            For Each entry In sortedEntries
                If Not String.IsNullOrWhiteSpace(entry.Item4) Then
                    Dim d = entry.Item1.Date
                    If Not bemerkungenProTag.ContainsKey(d) Then
                        bemerkungenProTag(d) = entry.Item4
                    ElseIf Not bemerkungenProTag(d).Contains(entry.Item4) Then
                        bemerkungenProTag(d) &= ", " & entry.Item4
                    End If
                End If
            Next

            For Each taggruppe In blocks.GroupBy(Function(b) b.Datum).OrderBy(Function(g) g.Key)
                Dim blocksThisDay = taggruppe.ToList()
                Dim tagName = blocksThisDay.First().TagName
                Dim datum = blocksThisDay.First().Datum
                Dim soll = If(tagName.ToLower() = "freitag", 6.0, 8.0)
                Dim bemerkungenText As String = If(bemerkungenProTag.ContainsKey(datum), bemerkungenProTag(datum), "")

                Dim pausenzeiten As New List(Of Tuple(Of TimeSpan, Double))
                If tagName.ToLower() = "freitag" Then
                    pausenzeiten.Add(Tuple.Create(New TimeSpan(9, 0, 0), 0.25))
                Else
                    pausenzeiten.Add(Tuple.Create(New TimeSpan(9, 0, 0), 0.25))
                    pausenzeiten.Add(Tuple.Create(New TimeSpan(12, 0, 0), 0.5))
                End If

                Dim pausenList As New List(Of Double)
                For Each block In blocksThisDay
                    Dim blockVon = block.Arbeitszeit.Von.TimeOfDay
                    Dim blockBis = block.Arbeitszeit.Bis.TimeOfDay
                    Dim pauseInBlock As Double = 0
                    For Each pause In pausenzeiten
                        If blockVon <= pause.Item1 AndAlso pause.Item1 < blockBis Then
                            pauseInBlock += pause.Item2
                        End If
                    Next
                    pausenList.Add(pauseInBlock)
                Next

                Dim arbeitszeiten = blocksThisDay.Select(Function(b, idx) Math.Max(0, (b.Arbeitszeit.Bis - b.Arbeitszeit.Von).TotalMinutes / 60 - pausenList(idx))).ToList()
                Dim fahrzeiten = blocksThisDay.Select(Function(b)
                                                          Dim ft As Double = 0
                                                          If b.Anfahrt.HasValue Then ft += (b.Anfahrt.Value.Bis - b.Anfahrt.Value.Von).TotalMinutes / 60
                                                          If b.Abfahrt.HasValue Then ft += (b.Abfahrt.Value.Bis - b.Abfahrt.Value.Von).TotalMinutes / 60
                                                          Return ft
                                                      End Function).ToList()
                Dim sumArbeitszeit = arbeitszeiten.Sum()
                Dim sumFahrzeit = fahrzeiten.Sum()
                Dim uebersoll = Math.Max(0, sumArbeitszeit - soll)
                Dim ueb25 = Math.Min(2, uebersoll)
                Dim ueb50 = Math.Max(0, uebersoll - 2)
                Dim verguetung = Math.Min(sumArbeitszeit, soll) + ueb25 * 1.25 + ueb50 * 1.5

                For idx = 0 To blocksThisDay.Count - 1
                    Dim b = blocksThisDay(idx)
                    Dim arbeitszeit = arbeitszeiten(idx)
                    Dim fahrzeit = fahrzeiten(idx)
                    Dim pause = pausenList(idx)
                    Dim anteil = If(sumArbeitszeit > 0, arbeitszeit / sumArbeitszeit, 0)
                    Dim ueber = uebersoll * anteil
                    Dim u25 = ueb25 * anteil
                    Dim u50 = ueb50 * anteil
                    Dim verg = verguetung * anteil

                    Dim fahrzeitStr As String = ""
                    If b.Anfahrt.HasValue Then
                        fahrzeitStr = $"{b.Anfahrt.Value.Von:HH:mm}–{b.Anfahrt.Value.Bis:HH:mm}"
                    End If
                    If b.Abfahrt.HasValue Then
                        If fahrzeitStr <> "" Then fahrzeitStr &= ", "
                        fahrzeitStr &= $"{b.Abfahrt.Value.Von:HH:mm}–{b.Abfahrt.Value.Bis:HH:mm}"
                    End If

                    tagesdaten.Rows.Add(
                    tagName,
                    datum.ToString("dd.MM.yyyy"),
                    b.Baustelle,
                    bemerkungenText,
                    fahrzeitStr,
                    $"{b.Arbeitszeit.Von:HH:mm}–{b.Arbeitszeit.Bis:HH:mm}",
                    pause.ToString("0.00"),
                    arbeitszeit.ToString("0.00"),
                    fahrzeit.ToString("0.00"),
                    ueber.ToString("0.00"),
                    u25.ToString("0.00"),
                    u50.ToString("0.00"),
                    verg.ToString("0.00"),
                    "OK"
                )
                Next
            Next

            Dim summenTabelle As New DataTable()
            summenTabelle.Columns.Add("Zeitraumtyp")
            summenTabelle.Columns.Add("Zeitraum")
            summenTabelle.Columns.Add("Arbeitszeit (h)")
            summenTabelle.Columns.Add("Fahrzeit (h)")
            summenTabelle.Columns.Add("Überstunden (h)")

            Dim monat = ""
            If tagesdaten.Rows.Count > 0 Then
                monat = DateTime.ParseExact(tagesdaten.Rows(0)("Datum").ToString(), "dd.MM.yyyy", Globalization.CultureInfo.InvariantCulture).ToString("MMMM yyyy", New Globalization.CultureInfo("de-DE"))
            End If
            Dim arbeitszeitGes = tagesdaten.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Vergütete Arbeitszeit (h)")))
            Dim fahrzeitGes = tagesdaten.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Fahrzeit (h)")))
            Dim ueberstundenGes = tagesdaten.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Überstunden (h)")))
            summenTabelle.Rows.Add("Monat", monat, arbeitszeitGes.ToString("0.00"), fahrzeitGes.ToString("0.00"), ueberstundenGes.ToString("0.00"))

            GridViewTagesdaten.DataSource = tagesdaten
            GridViewTagesdaten.DataBind()
            GridViewSummen.DataSource = summenTabelle
            GridViewSummen.DataBind()

            Session("Tagesdaten") = tagesdaten
            Session("SummenTabelle") = summenTabelle

        Catch ex As Exception
            lblStatus.Text = "Fehler: " & ex.Message
        End Try
    End Sub


    Protected Sub btnExportPDF_Click(sender As Object, e As EventArgs)
        Dim tagesdaten As DataTable = TryCast(Session("Tagesdaten"), DataTable)
        Dim summenTabelle As DataTable = TryCast(Session("SummenTabelle"), DataTable)
        If tagesdaten Is Nothing OrElse tagesdaten.Rows.Count = 0 Then
            lblStatus.Text = "Keine Daten zum Exportieren."
            Return
        End If

        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate, 20, 20, 20, 20)
        Dim ms As New IO.MemoryStream()
        iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms)
        doc.Open()

        Dim ersterMonat As String = DateTime.Now.ToString("MMMM", New Globalization.CultureInfo("de-DE"))
        Dim erstesJahr As String = DateTime.Now.Year.ToString()
        If tagesdaten.Rows.Count > 0 Then
            Dim d As DateTime
            If DateTime.TryParseExact(tagesdaten.Rows(0)("Datum").ToString(), "dd.MM.yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
                ersterMonat = d.ToString("MMMM", New Globalization.CultureInfo("de-DE"))
                erstesJahr = d.Year.ToString()
            End If
        End If

        doc.Add(New iTextSharp.text.Paragraph($"Stundenbericht {ersterMonat} {erstesJahr}", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 16)))
        doc.Add(New iTextSharp.text.Paragraph("Erstellt am: " & DateTime.Now.ToString("dd.MM.yyyy")))
        doc.Add(New iTextSharp.text.Paragraph(" "))
        doc.Add(New iTextSharp.text.Paragraph("Tagesdaten", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12)))

        ' Tabelle: Tagesdaten
        Dim pdfTable1 As New iTextSharp.text.pdf.PdfPTable(tagesdaten.Columns.Count)
        pdfTable1.WidthPercentage = 100
        For Each col As DataColumn In tagesdaten.Columns
            pdfTable1.AddCell(New iTextSharp.text.Phrase(col.ColumnName, iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 9)))
        Next
        For Each row As DataRow In tagesdaten.Rows
            For Each col As DataColumn In tagesdaten.Columns
                pdfTable1.AddCell(New iTextSharp.text.Phrase(row(col).ToString(), iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 9)))
            Next
        Next
        doc.Add(pdfTable1)

        doc.Add(New iTextSharp.text.Paragraph(" "))
        doc.Add(New iTextSharp.text.Paragraph("Summen", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12)))
        ' Tabelle: Summen
        Dim pdfTable2 As New iTextSharp.text.pdf.PdfPTable(summenTabelle.Columns.Count)
        pdfTable2.WidthPercentage = 100
        For Each col As DataColumn In summenTabelle.Columns
            pdfTable2.AddCell(New iTextSharp.text.Phrase(col.ColumnName, iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 9)))
        Next
        For Each row As DataRow In summenTabelle.Rows
            For Each col As DataColumn In summenTabelle.Columns
                pdfTable2.AddCell(New iTextSharp.text.Phrase(row(col).ToString(), iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 9)))
            Next
        Next
        doc.Add(pdfTable2)

        doc.Close()

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", $"attachment;filename=Stundenbericht_{ersterMonat}_{erstesJahr}.pdf")
        Response.BinaryWrite(ms.ToArray())
        Response.End()
    End Sub



    Protected Sub btnExportCSV_Click(sender As Object, e As EventArgs)
        Dim tagesdaten As DataTable = TryCast(Session("Tagesdaten"), DataTable)
        Dim summenTabelle As DataTable = TryCast(Session("SummenTabelle"), DataTable)
        If tagesdaten Is Nothing OrElse tagesdaten.Rows.Count = 0 Then
            lblStatus.Text = "Keine Daten zum Exportieren."
            Return
        End If

        Dim sb As New System.Text.StringBuilder()
        ' Tagesdaten
        For Each col As DataColumn In tagesdaten.Columns
            sb.Append(col.ColumnName & ";")
        Next
        sb.AppendLine()
        For Each row As DataRow In tagesdaten.Rows
            For Each col As DataColumn In tagesdaten.Columns
                sb.Append(row(col).ToString().Replace(";", ",") & ";")
            Next
            sb.AppendLine()
        Next
        sb.AppendLine()
        ' Summen
        For Each col As DataColumn In summenTabelle.Columns
            sb.Append(col.ColumnName & ";")
        Next
        sb.AppendLine()
        For Each row As DataRow In summenTabelle.Rows
            For Each col As DataColumn In summenTabelle.Columns
                sb.Append(row(col).ToString().Replace(";", ",") & ";")
            Next
            sb.AppendLine()
        Next

        Dim ersterMonat As String = DateTime.Now.ToString("MMMM", New Globalization.CultureInfo("de-DE"))
        Dim erstesJahr As String = DateTime.Now.Year.ToString()
        If tagesdaten.Rows.Count > 0 Then
            Dim d As DateTime
            If DateTime.TryParseExact(tagesdaten.Rows(0)("Datum").ToString(), "dd.MM.yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
                ersterMonat = d.ToString("MMMM", New Globalization.CultureInfo("de-DE"))
                erstesJahr = d.Year.ToString()
            End If
        End If

        Response.Clear()
        Response.ContentType = "text/csv"
        Response.AddHeader("content-disposition", $"attachment;filename=Stundenbericht_{ersterMonat}_{erstesJahr}.csv")
        Response.ContentEncoding = System.Text.Encoding.UTF8
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub GridViewTagesdaten_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridViewTagesdaten.EditIndex = e.NewEditIndex
        GridViewTagesdaten.DataSource = CType(Session("Tagesdaten"), DataTable)
        GridViewTagesdaten.DataBind()
    End Sub
    Protected Sub GridViewTagesdaten_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridViewTagesdaten.EditIndex = -1
        GridViewTagesdaten.DataSource = CType(Session("Tagesdaten"), DataTable)
        GridViewTagesdaten.DataBind()
    End Sub
    Protected Sub GridViewTagesdaten_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim dt As DataTable = CType(Session("Tagesdaten"), DataTable)
        Dim row As GridViewRow = GridViewTagesdaten.Rows(e.RowIndex)

        Dim datum As String = GridViewTagesdaten.DataKeys(e.RowIndex).Values("Datum").ToString()
        Dim baustelle As String = GridViewTagesdaten.DataKeys(e.RowIndex).Values("Baustelle").ToString()

        ' Indexe anpassen, falls du Spalten-Reihenfolge änderst!
        Dim bemerkung As String = CType(row.Cells(3).Controls(0), TextBox).Text
        Dim fahrzeitRaw As String = CType(row.Cells(4).Controls(0), TextBox).Text
        Dim arbeitszeitRaw As String = CType(row.Cells(5).Controls(0), TextBox).Text
        Dim pauseRaw As String = CType(row.Cells(6).Controls(0), TextBox).Text

        For Each dr As DataRow In dt.Rows
            If dr("Datum").ToString() = datum AndAlso dr("Baustelle").ToString() = baustelle Then
                dr("Bemerkung") = bemerkung
                dr("Fahrzeit-Zeitraum") = fahrzeitRaw
                dr("Arbeitszeit-Zeitraum") = arbeitszeitRaw
                dr("Pausenzeit (h)") = pauseRaw

                ' Arbeitszeit neu berechnen
                Try
                    Dim zeiten = arbeitszeitRaw.Split("–"c)
                    If zeiten.Length = 2 Then
                        Dim von = DateTime.ParseExact(zeiten(0).Trim(), "HH:mm", CultureInfo.InvariantCulture)
                        Dim bis = DateTime.ParseExact(zeiten(1).Trim(), "HH:mm", CultureInfo.InvariantCulture)
                        Dim pause = Double.Parse(pauseRaw.Replace(",", "."), CultureInfo.InvariantCulture)
                        Dim arbeitszeit = Math.Max(0, (bis - von).TotalMinutes / 60.0 - pause)
                        dr("Arbeitszeit (h)") = arbeitszeit.ToString("0.00")

                        ' Neuberechnung Überstunden, Überstd. 25/50%, Vergütete Arbeitszeit
                        Dim soll As Double = 8.0 ' Oder je nach Tag dynamisch berechnen
                        If datum.ToLower().Contains("freitag") Then soll = 6.0

                        Dim uebersoll = Math.Max(0, arbeitszeit - soll)
                        Dim ueb25 = Math.Min(2, uebersoll)
                        Dim ueb50 = Math.Max(0, uebersoll - 2)
                        Dim verg = Math.Min(arbeitszeit, soll) + ueb25 * 1.25 + ueb50 * 1.5

                        dr("Überstunden (h)") = uebersoll.ToString("0.00")
                        dr("Überstd. 25% (h)") = ueb25.ToString("0.00")
                        dr("Überstd. 50% (h)") = ueb50.ToString("0.00")
                        dr("Vergütete Arbeitszeit (h)") = verg.ToString("0.00")
                    End If
                Catch ex As Exception
                    dr("Datenintegrität") = "FEHLER"
                End Try

                Exit For
            End If
        Next

        Session("Tagesdaten") = dt
        GridViewTagesdaten.EditIndex = -1
        GridViewTagesdaten.DataSource = dt
        GridViewTagesdaten.DataBind()

        Call AktualisiereSummen(dt)
    End Sub
    Private Sub AktualisiereSummen(dt As DataTable)
        Dim summenTabelle As New DataTable()
        summenTabelle.Columns.Add("Zeitraumtyp")
        summenTabelle.Columns.Add("Zeitraum")
        summenTabelle.Columns.Add("Arbeitszeit (h)")
        summenTabelle.Columns.Add("Fahrzeit (h)")
        summenTabelle.Columns.Add("Überstunden (h)")

        If dt.Rows.Count > 0 Then
            Dim monat = DateTime.ParseExact(dt.Rows(0)("Datum").ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM yyyy", New CultureInfo("de-DE"))
            Dim arbeitszeitGes = dt.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Vergütete Arbeitszeit (h)")))
            Dim fahrzeitGes = dt.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Fahrzeit (h)")))
            Dim ueberstundenGes = dt.AsEnumerable().Sum(Function(r) Convert.ToDouble(r.Field(Of String)("Überstunden (h)")))
            summenTabelle.Rows.Add("Monat", monat, arbeitszeitGes.ToString("0.00"), fahrzeitGes.ToString("0.00"), ueberstundenGes.ToString("0.00"))
        End If

        Session("SummenTabelle") = summenTabelle
        GridViewSummen.DataSource = summenTabelle
        GridViewSummen.DataBind()
        lblStatus.Text = "Summen aktualisiert: " & Now.ToString("T:HH:mm:ss")
    End Sub
End Class
