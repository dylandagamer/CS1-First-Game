Public Class Form2
    Public death As Integer
    Public level(10) As Boolean
    Dim load As Integer
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Hide()
        form1.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Hide()
        Button2.Hide()
        Button3.Hide()
        Button4.Hide()
        Button5.Hide()
        Button6.Hide()
        Button7.Hide()
        Button7.Hide()
        Button8.Hide()
        Button9.Hide()
        Button10.Hide()
        Button11.Hide()

        If load = 0 Then
            For x = 2 To 4
                level(x) = False
            Next
            level(1) = True
            load = 1
        End If

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        End
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        End
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Label2.Hide()
        Label3.Hide()
        Label4.Hide()
        Label5.Hide()
        Button1.Show()
        If level(3) = True Then
            Button2.Show()
        End If
        If level(4) = True Then
            Button3.Show()
        End If
        If level(2) = True Then
            Button4.Show()
        End If
        'Button5.Show()
        'Button6.Show()
        'Button7.Show()
        'Button8.Show()
        'Button9.Show()
        'Button10.Show()
        Button11.Show()

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Button1.Hide()
        Button2.Hide()
        Button3.Hide()
        Button4.Hide()
        Button5.Hide()
        Button6.Hide()
        Button7.Hide()
        Button7.Hide()
        Button8.Hide()
        Button9.Hide()
        Button10.Hide()
        Button11.Hide()

        Label2.Show()
        Label3.Show()
        Label4.Show()
        Label5.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        level2.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        level4.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        level3.Show()
        Me.Hide()
    End Sub
End Class