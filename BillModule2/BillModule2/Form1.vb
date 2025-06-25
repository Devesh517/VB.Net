Public Class Form1
    Dim tqty As Integer
    Dim namt, wgst, gst As Double
    Dim paystatus As String = ""
    Private isDarkMode As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = "Pay No"
        Button2.Text = "Remove"
        Dim f As New Font("calibri", 25)
        Button1.Font = f
        Button2.Font = f
        Button1.Enabled = False
        Button2.Enabled = False
        Panel2.Visible = False
        Panel1.Visible = False
        Button4.Text = "Cash"
        Button11.Text = "Pay"
        Button12.Text = "Pay    "
        Button5.Text = "UPI/QR"
        Button6.Text = "Credit/Debit Card"
        Button7.Text = "Pay"
        Label4.Text = "Scan QR"
        Panel3.Visible = False
        Label5.Text = "Enter Card Number"
        Label6.Text = "Expiry Date"
        Label7.Text = "CVV/CVC"
        TextBox10.Visible = False
        Button7.Enabled = False
        Label2.Visible = False
        Label2.Text = " "
        Label3.Text = "Enter UPI ID:"
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
               Label1.Text = TextBox9.Text
        Panel1.Left = (Me.Width - Panel1.Width) / 2
        Panel1.Top = (Me.Height - Panel1.Height) / 2
        Panel1.Visible = True
    End Sub
    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox1.Text <> "" Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim p As Double
        Dim status As Boolean = False
        If TextBox1.Text = "20250001" Then
            TextBox2.Text = "Kurkure"
            p = 50
            status = True
        ElseIf TextBox1.Text = "20250002" Then
            TextBox2.Text = "Cold Drink"
            p = 100
            status = True
        ElseIf TextBox1.Text = "20250003" Then
            TextBox2.Text = "Choclate"
            p = 150
            status = True
        ElseIf TextBox1.Text = "20250004" Then
            TextBox2.Text = "Chips"
            p = 30
            status = True
        End If
        Dim upstat As Boolean = False
        If status = True Then
            Dim l As ListViewItem
            Dim i As Integer
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).SubItems(0).Text = TextBox1.Text Then
                    l = ListView1.Items(i)
                    l.SubItems(3).Text = Val(l.SubItems(3).Text) + 1
                    l.SubItems(4).Text = Val(l.SubItems(3).Text) * p
                    upstat = True
                End If
            Next
            If upstat = False Then
                l = ListView1.Items.Add(TextBox1.Text)
                l.SubItems.Add(TextBox2.Text)
                l.SubItems.Add(p)
                l.SubItems.Add(1)
                l.SubItems.Add(p * 1)
            End If
            namt = amountTotal()
            TextBox8.Text = Format((namt * (10 / 100)), "0.00")
            namt = namt - (namt * (10 / 100))
            wgst = (namt / 105) * 100
            TextBox5.Text = Format(wgst, "0.00")
            TextBox6.Text = Format((namt - wgst) / 2, "0.00")
            TextBox7.Text = Format((namt - wgst) / 2, "0.00")
            TextBox9.Text = namt
            TextBox3.Text = tqty
            TextBox4.Text = ListView1.Items.Count
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox1.Focus()
        End If
    End Sub
    Function amountTotal()
        tqty = 0
        Dim i As Integer, tamt As Double = 0
        For i = 0 To ListView1.Items.Count - 1
            tamt += ListView1.Items(i).SubItems(4).Text
            tqty += ListView1.Items(i).SubItems(3).Text
        Next
        Return tamt
    End Function
    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        If ListView1.SelectedItems(0).SubItems.Count <> 0 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim m As Integer
        m = MsgBox("Do you want to delete item.. ", MsgBoxStyle.YesNo)
        If m = 6 Then
            ListView1.Items.RemoveAt(ListView1.SelectedItems(0).Index)
            namt = amountTotal()
            TextBox5.Text = Format(wgst, "0.00")
            TextBox6.Text = Format((namt - wgst) / 2, "0.00")
            TextBox7.Text = Format((namt - wgst) / 2, "0.00")
            TextBox9.Text = namt
            TextBox3.Text = tqty
            TextBox4.Text = ListView1.Items.Count
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        paystatus = "Cash"
        TextBox10.Visible = True
        Label2.Visible = True
        TextBox10.Focus()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        paystatus = "UPI"
        Panel2.Visible = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        paystatus = "Credit/Debit Card"
        Panel3.Visible = True
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Panel1.Visible = False
        paystatus = ""
        Button4.Focus()
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        Dim amt As Double
        amt = Val(TextBox10.Text) - Val(Label1.Text)
        If amt < 0 Then
            Label2.Text = amt
            Label2.ForeColor = Color.Red
            Button7.Enabled = False
        Else
            Label2.Text = amt
            Label2.ForeColor = Color.Green
            Button7.Enabled = True
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MsgBox("Payment Successful")
        Panel1.Visible = False
        ListView1.Items.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If isDarkMode = False Then
            Me.BackColor = Color.FromArgb(30, 30, 30)
            Button3.BackColor = Color.DimGray
            Button3.ForeColor = Color.White
            Label1.ForeColor = Color.White
            ListView1.BackColor = Color.Black
            ListView1.ForeColor = Color.White
            isDarkMode = True
        Else
            Me.BackColor = Color.White
            Button3.BackColor = Color.LightGray
            Button3.ForeColor = Color.Black
            Label1.ForeColor = Color.Black
            ListView1.BackColor = Color.White
            ListView1.ForeColor = Color.Black
            isDarkMode = False
        End If
    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel2.Visible = False
        paystatus = ""
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Panel3.Visible = False
        paystatus = ""
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        MsgBox("Payment Successfull")
        Panel3.Visible = False
        ListView1.Items.Clear()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MsgBox("Payment Successfull")
        Panel2.Visible = False
        ListView1.Items.Clear()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged

    End Sub
End Class
