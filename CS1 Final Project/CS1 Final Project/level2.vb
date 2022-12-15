Imports System.Numerics
Imports System.Reflection.Emit

Public Class level2
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery As Integer
    Public Level As Integer
    Dim jumping, win, obst1R, obst2R As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump
    Dim left, right, obstleft, leveron, platup, overide As Boolean

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        If jumping = True Then
            PlayerY = PlayerY - jumpmove
            jumpmove = jumpmove - 1
        End If

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

        collisions()

        drawframe()
    End Sub

    Private Sub level2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form1.Close()
form2.level(2) = true

        PlayerY = GH() - 50
        PlayerX = 75
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False

        jumpheight = 15

        platX(1) = 250
        platY(1) = 700

        platX(2) = 575
        platY(2) = 625

        platX(3) = 875
        platY(3) = 575

        platX(4) = 575
        platY(4) = 500

        platX(5) = 250
        platY(5) = 425

        obstX(1) = platX(2)
        obstY(1) = platY(2) - (28 / 2)

        obstX(2) = platX(4)
        obstY(2) = platY(4) - (28 / 2)

        leverx = platX(3) + 75 - (25 / 2)
        levery = platY(3) - 25

        win = False

        Timer1.Start()
    End Sub

    Sub collisions()
        Dim plat As New Rectangle(platX(1), platY(1), 150, 25)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim saw As New Rectangle(obstX(1), obstY(1), 30, 30)
        Dim saw2 As New Rectangle(obstX(2), obstY(2), 30, 30)
        Dim plat2 As New Rectangle(platX(2), platY(2), 150, 25)
        Dim plat3 As New Rectangle(platX(3), platY(3), 150, 25)
        Dim plat4 As New Rectangle(platX(4), platY(4), 150, 25)
        Dim plat5 As New Rectangle(platX(5), platY(5), 150, 25)

        If player.IntersectsWith(plat) Then
            jumping = False
            If PlayerY <= platY(1) Then
                PlayerY = platY(1) - 24
                jumpmove = 0
                overide = False
            Else
                PlayerY = platY(1) + 26
                jumpmove = -1
                jumping = True
                overide = False
            End If
        ElseIf PlayerY >= GH() - 50 Then
            jumping = False
            PlayerY = GH() - 50
            overide = False
        ElseIf player.IntersectsWith(plat2) Then
            jumping = False
            If PlayerY <= platY(2) Then
                PlayerY = platY(2) - 24
                jumpmove = 0
                overide = False
            Else
                PlayerY = platY(2) + 26
                jumpmove = -1
                jumping = True
                overide = False
            End If
        ElseIf player.IntersectsWith(plat3) Then
            jumping = False
            If PlayerY <= platY(3) Then
                PlayerY = platY(3) - 24
                jumpmove = 0
                overide = False
            Else
                PlayerY = platY(3) + 26
                jumpmove = -1
                jumping = True
                overide = False
            End If
        ElseIf player.IntersectsWith(plat4) Then
            jumping = False
            If PlayerY <= platY(4) Then
                PlayerY = platY(4) - 24
                jumpmove = 0
                overide = False
            Else
                PlayerY = platY(4) + 26
                jumpmove = -1
                jumping = True
                overide = False
            End If
        ElseIf player.IntersectsWith(plat5) Then
            jumping = False
            If PlayerY <= platY(5) Then
                PlayerY = platY(5) - 24
                jumpmove = 0
                overide = False
            Else
                PlayerY = platY(5) + 26
                jumpmove = -1
                jumping = True
                overide = False
            End If
        Else
            jumping = True
        End If

        If player.IntersectsWith(saw) Then
            PlayerY = GH() - 50
            PlayerX = 75
            Form2.death = Form2.death + 1
            Label1.Text = ": " & Form2.death
            leveron = False
        End If
        If player.IntersectsWith(saw2) Then
            PlayerY = GH() - 50
            PlayerX = 75
            Form2.death = Form2.death + 1
            Label1.Text = ": " & Form2.death
            leveron = False
        End If
    End Sub

    Sub drawframe()
        canvaspen.Clear(Color.Black)

        canvaspen.DrawImage(My.Resources.skull, 0, 0, 50, 50)

        If leveron = False Then
            canvaspen.DrawImage(My.Resources.Door, platX(5) + 50, platY(5) - 70, 50, 75)
        Else
            canvaspen.DrawImage(My.Resources.door2, platX(5) + 50, platY(5) - 70, 70, 75)
        End If

        If leveron = True Then
            canvaspen.DrawImage(My.Resources.Lever2, leverx, levery, 25, 25)
        Else
            canvaspen.DrawImage(My.Resources.Lever, leverx, levery, 25, 25)
        End If

        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)

        If obstX(1) <= platX(2) Then
            obst1R = True
        ElseIf obstX(1) >= platX(2) + 115 Then
            obst1R = False
        End If

        If obst1R = True Then
            obstX(1) = obstX(1) + 4
        ElseIf obst1R = False Then
            obstX(1) = obstX(1) - 4
        End If

        If obstX(2) <= platX(4) Then
            obst2R = True
        ElseIf obstX(2) >= platX(4) + 115 Then
            obst2R = False
        End If

        If obst2R = True Then
            obstX(2) = obstX(2) + 4
        ElseIf obst2R = False Then
            obstX(2) = obstX(2) - 4
        End If

        If frame = 0 Or frame = 1 Then
            canvaspen.DrawImage(My.Resources.S1, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S1, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 2 Or frame = 3 Then
            canvaspen.DrawImage(My.Resources.S2, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S2, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 4 Or frame = 5 Then
            canvaspen.DrawImage(My.Resources.S3, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S3, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 6 Then
            frame = 0
            canvaspen.DrawImage(My.Resources.S4, obstX(1), obstY(1), 35, 35)
            canvaspen.DrawImage(My.Resources.S4, obstX(2), obstY(2), 35, 35)
        End If


        canvaspen.FillRectangle(Brushes.Blue, platX(1), platY(1), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(2), platY(2), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(3), platY(3), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(4), platY(4), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(5), platY(5), 150, 25)
        canvaspen.FillRectangle(Brushes.Blue, 0, GH() - 25, GW, 25)


        formpen.DrawImage(canvas, 0, 0)

        Label1.Text = ": " & Form2.death
    End Sub
    Private Sub level2_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub level2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim Door As New Rectangle(platX(5) + 50, platY(5) - 70, 50, 75)
        Dim lever As New Rectangle(leverx - 25, levery - 25, 50, 50)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)

        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If

        If e.KeyChar = " " And player.IntersectsWith(Door) And leveron = True Then
            level4.Show()
            win = True
            Me.Close()
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = False Then
            leveron = True
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = True Then
            leveron = False
        End If

    End Sub

    Private Sub level2_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub
    Function GH()
        Return Me.ClientSize.Height
    End Function
    Function GW()
        Return Me.ClientSize.Width
    End Function

    Private Sub level2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If win = False Then
            Form2.Show()
        End If
    End Sub
End Class