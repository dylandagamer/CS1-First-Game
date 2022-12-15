Imports System.Numerics
Imports System.Reflection.Emit

Public Class Form4
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery As Integer
    Public Level As Integer

    Dim left, right, obstleft, leveron, platup, overide As Boolean

    Dim jumping As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PlayerY = GH() - 50
        PlayerX = 75
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False
        jumpheight = 15

        platX(1) = 750
        platY(1) = GH() - 140

        obstX(1) = 350
        obstY(1) = GH() - 50

        platX(2) = 1000
        platY(2) = GH() - 200

        Timer1.Start()

    End Sub

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

        collisions()

        drawframe()

    End Sub
    Sub collisions()
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim obstacle As New Rectangle(obstX(1), obstY(1), 35, 35)
        Dim platform As New Rectangle(platX(1), platY(1), 150, 25)
        Dim platform2 As New Rectangle(platX(2), platY(2), 300, 25)
        Dim zone2 As New Rectangle(platX(1) - 125, 0, GW, GH)

        If PlayerY >= GH() - 50 Then
            PlayerY = GH() - 50
            jumping = False
        ElseIf player.IntersectsWith(platform) Then
            If PlayerY < platY(1) Then
                jumping = False
                PlayerY = platY(1) - 24
                jumpmove = 0
            Else
                PlayerY = platY(1) + 25
                jumpmove = -1
            End If
        ElseIf player.IntersectsWith(platform2) Then
            If PlayerY < platY(2) Then
                jumping = False
                PlayerY = platY(2) - 24
                jumpmove = 0
            Else
                PlayerY = platY(2) + 25
                jumpmove = -1
            End If
        Else
            jumping = True
        End If

        If player.IntersectsWith(obstacle) Then
            PlayerY = GH() - 50
            PlayerX = 75
        End If

        If player.IntersectsWith(zone2) Then
            Label2.Text = "Good Luck!"
        Else
            Label2.Text = "Running into saws kills your character"
        End If

    End Sub

    Sub drawframe()

        canvaspen.Clear(Color.Black)
        If leveron = False Then
            canvaspen.DrawImage(My.Resources.Door, platX(2) + 100, platY(2) - 70, 50, 75)
        Else
            canvaspen.DrawImage(My.Resources.door2, platX(2) + 100, platY(2) - 70, 70, 75)
        End If

        If leveron = True Then
            canvaspen.DrawImage(My.Resources.Lever2, platX(1) + 50, platY(1) - 25, 25, 25)
        Else
            canvaspen.DrawImage(My.Resources.Lever, platX(1) + 50, platY(1) - 25, 25, 25)
        End If
        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(1), platY(1), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(2), platY(2), 300, 25)
        If frame = 0 Or frame = 1 Then
            canvaspen.DrawImage(My.Resources.S1, obstX(1), obstY(1), 35, 35)
            frame = frame + 1
        ElseIf frame = 2 Or frame = 3 Then
            canvaspen.DrawImage(My.Resources.S2, obstX(1), obstY(1), 35, 35)
            frame = frame + 1
        ElseIf frame = 4 Or frame = 5 Then
            canvaspen.DrawImage(My.Resources.S3, obstX(1), obstY(1), 35, 35)
            frame = frame + 1
        ElseIf frame = 6 Then
            frame = 0
            canvaspen.DrawImage(My.Resources.S4, obstX(1), obstY(1), 35, 35)
        End If
        canvaspen.FillRectangle(Brushes.Blue, 0, GH() - 25, GW, 25)

        formpen.DrawImage(canvas, 0, 0)

    End Sub
    Function GH()
        Return Me.ClientSize.Height
    End Function
    Function GW()
        Return Me.ClientSize.Width
    End Function

    Private Sub Form4_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub Form4_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub

    Private Sub Form4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If

        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim door As New Rectangle(platX(2) + 100, platY(2) - 70, 50, 75)
        Dim lever As New Rectangle(platX(1) + 50, platY(1) - 50, 50, 50)

        If e.KeyChar = " " And player.IntersectsWith(door) And leveron = True Then
            Me.Close()
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = False Then
            leveron = True
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = True Then
            leveron = False
        End If
    End Sub

    Private Sub Form4_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form2.Show()
    End Sub
End Class