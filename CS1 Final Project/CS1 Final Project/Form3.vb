Imports System.Data.Common
Imports System.Reflection.Emit
Public Class Form3
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery As Integer

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If left = True Then
            If PlayerX > 0 Then
                PlayerX = PlayerX - 7
            End If
        End If

        If right = True Then
            If PlayerX <= GW() - 25 Then
                PlayerX = PlayerX + 7
            End If
        End If

        If jumping = True Then
            PlayerY = PlayerY - jumpmove
            jumpmove = jumpmove - 1
        End If

        collsions()

        drawframe()
    End Sub
    Sub collsions()
        Dim Wall As New Rectangle(750, GH() - 100, 25, 150)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim zone1 As New Rectangle(0, 0, 750, GH)

        If PlayerY >= GH() - 50 Then
            PlayerY = GH() - 50
            jumping = False
        ElseIf PlayerX > 725 And playerx < 775 And PlayerY >= GH - 125 Then
            If PlayerY <= GH() - 125 Then
                PlayerY = GH() - 125
                jumping = False
                jumpmove = 0
            End If
        Else
            jumping = True
        End If

        If player.IntersectsWith(Wall) Then
            If PlayerX > 725 And PlayerX < 749 Then
                If PlayerY > GH() - 120 Then
                    right = False
                    PlayerX = 725
                End If
            ElseIf PlayerX < 775 Then
                If PlayerY > GH() - 120 Then
                    left = False
                    PlayerX = 775
                End If
            End If
        End If

        If player.IntersectsWith(zone1) Then
            Label1.Text = "A and S to move left and right" & vbCrLf & "W to jump"
        Else
            Label1.Text = "Space to interact with doors and levers"
        End If
    End Sub
    Sub drawframe()

        canvaspen.Clear(Color.Black)
        canvaspen.DrawImage(My.Resources.door2, 1200, GH() - 95, 70, 75)
        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)
        canvaspen.FillRectangle(Brushes.Blue, 0, GH() - 25, GW, 25)
        canvaspen.FillRectangle(Brushes.Blue, 750, GH() - 100, 25, 150)

        formpen.DrawImage(canvas, 0, 0)
    End Sub

    Public Level As Integer

    Dim left, right, obstleft, leveron, platup, overide, levelcomplete As Boolean

    Dim jumping As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PlayerY = GH() - 50
        PlayerX = 75
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False
        jumpheight = 15
        levelcomplete = False
        Timer1.Start()
    End Sub
    Function GH()
        Return Me.ClientSize.Height
    End Function
    Function GW()
        Return Me.ClientSize.Width
    End Function
    Private Sub Form3_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub Form3_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub

    Private Sub Form3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If levelcomplete = False Then
            Form2.Show()
        End If
    End Sub

    Private Sub Form3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If

        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim door As New Rectangle(1200, GH() - 95, 70, 75)

        If e.KeyChar = " " And player.IntersectsWith(door) Then
            levelcomplete = True
            Form4.Show()
            Me.Close()
        End If
    End Sub
End Class