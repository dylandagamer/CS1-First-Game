Imports System.Numerics

Public Class level3
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery, obst1jump, obst2jump As Integer
    Public Level As Integer

    Dim left, right, obstleft, leveron, platup, overide, win As Boolean

    Dim jumping As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump
    Private Sub level3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PlayerY = GH() - 50
        PlayerX = 75
        Level = 1
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False
form2.level(4) = true
        jumpheight = 15

        platX(1) = 300
        platY(1) = 700

        platX(2) = 575
        platY(2) = 650

        platX(3) = 850
        platY(3) = 600

        platX(4) = 1100
        platY(4) = 550

        obstX(1) = platX(2)
        obstY(1) = platY(2) - 25

        obstX(2) = platX(2) + 110
        obstY(2) = platY(2) - 75

        obst1jump = 23
        obst2jump = 0

        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If left = True Then
            If PlayerX > 0 Then
                PlayerX = PlayerX - 7
            End If
        End If

        If right = True Then
            If PlayerX < GW() - 25 Then
                PlayerX = PlayerX + 7
            End If
        End If

        If jumping = True Then
            PlayerY = PlayerY - jumpmove
            jumpmove = jumpmove - 1
        End If

        If obstY(1) >= platY(2) - 25 Then
            obst1jump = 23
            obstY(1) = obstY(1) - obst1jump
        Else
            obstY(1) = obstY(1) - obst1jump
            obst1jump = obst1jump - 1
        End If


        If obstY(2) >= platY(2) - 25 Then
            obst1jump = 23
            obstY(2) = obstY(2) - obst1jump
        Else
            obstY(2) = obstY(2) - obst1jump
            obst1jump = obst1jump - 1
        End If


        collision()

        drawframe()

    End Sub

    Private Sub level3_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub level3_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub

    Private Sub level3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim lever As New Rectangle(platX(4) + 25, platY(4) - 50, 50, 50)
        Dim door As New Rectangle(50, GH() - 95, 50, 75)
        If e.KeyChar = " " And player.IntersectsWith(lever) Then
            If leveron = True Then
                leveron = False
            Else
                leveron = True
            End If
        ElseIf player.IntersectsWith(door) And leveron = True Then
            win = True
            Me.Close()
        End If
    End Sub
    Function GH()
        Return Me.ClientSize.Height
    End Function
    Function GW()
        Return Me.ClientSize.Width
    End Function

    Sub collision()
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim plat1 As New Rectangle(platX(1), platY(1), 125, 25)
        Dim plat2 As New Rectangle(platX(2), platY(2), 150, 25)
        Dim plat3 As New Rectangle(platX(3), platY(3), 125, 25)
        Dim plat4 As New Rectangle(platX(4), platY(4), 150, 25)
        Dim obst As New Rectangle(platX(1) + 50, platY(1) - 25, 35, 35)
        Dim obst2 As New Rectangle(obstX(1), obstY(1), 35, 35)
        Dim obst3 As New Rectangle(obstX(2), obstY(2), 35, 35)

        If PlayerY >= GH() - 50 And PlayerX <= platX(2) Then
            PlayerY = GH() - 50
            jumping = False
        ElseIf player.IntersectsWith(plat1) Then
            If PlayerY < platY(1) Then
                PlayerY = platY(1) - 24
                jumping = False
                jumpmove = 0
            Else
                PlayerY = platY(1) + 25
                jumpmove = -1
            End If
        ElseIf player.IntersectsWith(plat2) Then
            If PlayerY < platY(2) Then
                PlayerY = platY(2) - 24
                jumping = False
                jumpmove = 0
            Else
                PlayerY = platY(2) + 25
                jumpmove = -1
            End If
        ElseIf player.IntersectsWith(plat3) Then
            If PlayerY < platY(3) Then
                PlayerY = platY(3) - 24
                jumping = False
                jumpmove = 0
            Else
                PlayerY = platY(3) + 25
                jumpmove = -1
            End If
        ElseIf player.IntersectsWith(plat4) Then
            If PlayerY < platY(4) Then
                PlayerY = platY(4) - 24
                jumping = False
                jumpmove = 0
            Else
                PlayerY = platY(4) + 25
                jumpmove = -1
            End If
        Else
            jumping = True
        End If

        If player.IntersectsWith(obst) Then
            Form2.death = Form2.death + 1
            PlayerY = GH() - 50
            PlayerX = 75
            leveron = False
        ElseIf player.IntersectsWith(obst2) Then
            Form2.death = Form2.death + 1
            PlayerY = GH() - 50
            PlayerX = 75
            leveron = False
        ElseIf player.IntersectsWith(obst3) Then
            Form2.death = Form2.death + 1
            PlayerY = GH() - 50
            PlayerX = 75
            leveron = False
        End If

        If PlayerY > GH() Then
            Form2.death = Form2.death + 1
            PlayerY = GH() - 50
            PlayerX = 75
            leveron = False
        End If

    End Sub

    Sub drawframe()

        canvaspen.Clear(Color.Black)
        canvaspen.DrawImage(My.Resources.skull, 0, 0, 50, 50)
        If leveron = False Then
            canvaspen.DrawImage(My.Resources.Door, 50, GH() - 95, 50, 75)
            canvaspen.DrawImage(My.Resources.Lever, platX(4) + 50, platY(4) - 25, 25, 25)
        Else
            canvaspen.DrawImage(My.Resources.door2, 50, GH() - 95, 70, 75)
            canvaspen.DrawImage(My.Resources.Lever2, platX(4) + 50, platY(4) - 25, 25, 25)
        End If
        If frame <= 3 Then
            canvaspen.DrawImage(My.Resources.S1, platX(1) + 50, platY(1) - 25, 35, 35)
            canvaspen.DrawImage(My.Resources.S1, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S1, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame > 3 And frame <= 6 Then
            canvaspen.DrawImage(My.Resources.S2, platX(1) + 50, platY(1) - 25, 35, 35)
            canvaspen.DrawImage(My.Resources.S2, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S2, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame > 6 And frame <= 9 Then
            canvaspen.DrawImage(My.Resources.S3, platX(1) + 50, platY(1) - 25, 35, 35)
            canvaspen.DrawImage(My.Resources.S3, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S3, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame > 9 And frame <= 12 Then
            canvaspen.DrawImage(My.Resources.S4, platX(1) + 50, platY(1) - 25, 35, 35)
            canvaspen.DrawImage(My.Resources.S4, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S4, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
            If frame = 13 Then
                frame = 0
            End If
        End If
        canvaspen.FillRectangle(Brushes.Blue, platX(1), platY(1), 125, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(2), platY(2), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(3), platY(3), 125, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(4), platY(4), 150, 25)
        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)
        canvaspen.FillRectangle(Brushes.Blue, 0, GH() - 25, platX(2), 25)

        formpen.DrawImage(canvas, 0, 0)

        Label1.Text = ": " & Form2.death
    End Sub

    Private Sub level3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If win = False Then
            Form2.Show()
        Else
            Form2.Show()
        End If
    End Sub
End Class