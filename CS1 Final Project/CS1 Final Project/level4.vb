Imports System.Threading

Public Class level4
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery, obstaclejump(10) As Integer
    Public Level As Integer

    Dim left, right, obstleft(10), leveron, platup, overide As Boolean

    Dim jumping, win As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump


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

        sawmovementx()
        sawmovementy()

        collisions()

        drawframe()
    End Sub

    Sub sawmovementy()

        If obstY(1) >= platY(1) - 25 Then
            obstaclejump(1) = 10
            obstY(1) = obstY(1) - obstaclejump(1)
        Else
            obstY(1) = obstY(1) - obstaclejump(1)
            obstaclejump(1) = obstaclejump(1) - 1
        End If

        If obstY(2) >= platY(2) - 25 Then
            obstaclejump(2) = 11
            obstY(2) = obstY(2) - obstaclejump(2)
        Else
            obstY(2) = obstY(2) - obstaclejump(2)
            obstaclejump(2) = obstaclejump(2) - 1
        End If

        If obstY(3) >= platY(3) - 25 Then
            obstaclejump(3) = 12
            obstY(3) = obstY(3) - obstaclejump(3)
        Else
            obstY(3) = obstY(3) - obstaclejump(3)
            obstaclejump(3) = obstaclejump(3) - 1
        End If

        If obstY(4) >= GH() - 60 Then
            obstaclejump(4) = 13
            obstY(4) = obstY(4) - obstaclejump(4)
        Else
            obstY(4) = obstY(4) - obstaclejump(4)
            obstaclejump(4) = obstaclejump(4) - 1
        End If

    End Sub

    Sub sawmovementx()

        If obstX(1) >= platX(1) + 265 Then
            obstleft(1) = True
        ElseIf obstX(1) <= platX(1) Then
            obstleft(1) = False
        End If

        If obstX(2) >= platX(2) + 265 Then
            obstleft(2) = True
        ElseIf obstX(2) <= platX(2) Then
            obstleft(2) = False
        End If

        If obstX(3) >= platX(3) + 265 Then
            obstleft(3) = True
        ElseIf obstX(3) <= platX(3) Then
            obstleft(3) = False
        End If

        If obstleft(1) = True Then
            obstX(1) = obstX(1) - 7
        Else
            obstX(1) = obstX(1) + 7
        End If

        If obstleft(2) = True Then
            obstX(2) = obstX(2) - 7
        Else
            obstX(2) = obstX(2) + 7
        End If

        If obstleft(3) = True Then
            obstX(3) = obstX(3) - 7
        Else
            obstX(3) = obstX(3) + 7
        End If
    End Sub

    Private Sub level4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = ": " & Form2.death
        Form2.level(3) = True
        PlayerY = GH() - 50
        PlayerX = 75
        Level = 1
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False

        jumpheight = 15

        platY(1) = GH() - 125
        platX(1) = 300

        platY(2) = 650
        platX(2) = 750

        platY(3) = 535
        platX(3) = 450

        platY(4) = 500
        platX(4) = 0

        obstX(1) = platX(1) + 75 - (25 / 2)
        obstY(1) = platY(1) - (25 / 2)

        obstX(2) = platX(2)
        obstY(2) = platY(2) - (25 / 2)

        obstX(3) = platX(3)
        obstY(3) = platY(3) - (25 / 2)

        obstX(4) = platX(2) + 525
        obstY(4) = GH() - (75 / 2)

        leverx = platX(4) + 50
        levery = platY(4) - 25

        win = False

        obstaclejump(1) = 10
        obstaclejump(3) = 12

        Timer1.Start()
    End Sub

    Sub collisions()
        Dim plat As New Rectangle(platX(1), platY(1), 300, 25)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim saw As New Rectangle(obstX(1), obstY(1), 30, 30)
        Dim saw2 As New Rectangle(obstX(2), obstY(2), 30, 30)
        Dim saw3 As New Rectangle(obstX(3), obstY(3), 30, 30)
        Dim saw4 As New Rectangle(obstX(4), obstY(4), 30, 30)
        Dim plat2 As New Rectangle(platX(2), platY(2), 300, 25)
        Dim plat3 As New Rectangle(platX(3), platY(3), 300, 25)
        Dim plat4 As New Rectangle(platX(4), platY(4), 375, 25)

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
            jumpmove = 0
            jumping = False
            If PlayerY > platY(4) Then
                PlayerY = platY(4) + 26
            Else
                PlayerY = platY(4) - 24
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
        ElseIf player.IntersectsWith(saw2) Then
            PlayerY = GH() - 50
            PlayerX = 75
            Form2.death = Form2.death + 1
            Label1.Text = ": " & Form2.death
            leveron = False
        ElseIf player.IntersectsWith(saw3) Then
            PlayerY = GH() - 50
            PlayerX = 75
            Form2.death = Form2.death + 1
            Label1.Text = ": " & Form2.death
            leveron = False
        ElseIf player.IntersectsWith(saw4) Then
            PlayerY = GH() - 50
            PlayerX = 75
            Form2.death = Form2.death + 1
            Label1.Text = ": " & Form2.death
            leveron = False
        End If

    End Sub

    Sub drawframe()

        canvaspen.Clear(Color.Black)
        If leveron = False Then
            canvaspen.DrawImage(My.Resources.Door, platX(2) + 600, GH() - 100, 50, 75)
        Else
            canvaspen.DrawImage(My.Resources.door2, platX(2) + 600, GH() - 100, 70, 75)
        End If

        If leveron = True Then
            canvaspen.DrawImage(My.Resources.Lever2, leverx, levery, 25, 25)
        Else
            canvaspen.DrawImage(My.Resources.Lever, leverx, levery, 25, 25)
        End If

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

        If frame = 0 Or frame = 1 Then
            canvaspen.DrawImage(My.Resources.S1, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 2 Or frame = 3 Then
            canvaspen.DrawImage(My.Resources.S2, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 4 Or frame = 5 Then
            canvaspen.DrawImage(My.Resources.S3, obstX(2), obstY(2), 35, 35)
            frame = frame + 1
        ElseIf frame = 6 Then
            frame = 0
            canvaspen.DrawImage(My.Resources.S4, obstX(2), obstY(2), 35, 35)
        End If

        If frame = 0 Or frame = 1 Then
            canvaspen.DrawImage(My.Resources.S1, obstX(3), obstY(3), 35, 35)
            frame = frame + 1
        ElseIf frame = 2 Or frame = 3 Then
            canvaspen.DrawImage(My.Resources.S2, obstX(3), obstY(3), 35, 35)
            frame = frame + 1
        ElseIf frame = 4 Or frame = 5 Then
            canvaspen.DrawImage(My.Resources.S3, obstX(3), obstY(3), 35, 35)
            frame = frame + 1
        ElseIf frame = 6 Then
            frame = 0
            canvaspen.DrawImage(My.Resources.S4, obstX(3), obstY(3), 35, 35)
        End If

        If frame = 0 Or frame = 1 Then
            canvaspen.DrawImage(My.Resources.S1, obstX(4), obstY(4), 35, 35)
            frame = frame + 1
        ElseIf frame = 2 Or frame = 3 Then
            canvaspen.DrawImage(My.Resources.S2, obstX(4), obstY(4), 35, 35)
            frame = frame + 1
        ElseIf frame = 4 Or frame = 5 Then
            canvaspen.DrawImage(My.Resources.S3, obstX(4), obstY(4), 35, 35)
            frame = frame + 1
        ElseIf frame = 6 Then
            frame = 0
            canvaspen.DrawImage(My.Resources.S4, obstX(4), obstY(4), 35, 35)
        End If

        canvaspen.FillRectangle(Brushes.Blue, platX(3), platY(3), 300, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(1), platY(1), 300, 25)
        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(2), platY(2), 300, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(4), platY(4), 375, 25)
        canvaspen.DrawImage(My.Resources.skull, 0, 0, 50, 50)


        canvaspen.FillRectangle(Brushes.Blue, 0, GH() - 25, GW, 25)


        formpen.DrawImage(canvas, 0, 0)
    End Sub

    Function GH()
        Return Me.ClientSize.Height
    End Function

    Function GW()
        Return Me.ClientSize.Width
    End Function

    Private Sub level4_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub level4_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub

    Private Sub level4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim door As New Rectangle(platX(2) + 600, GH() - 100, 50, 75)
        Dim lever As New Rectangle(leverx - 25, levery - 25, 50, 50)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)

        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If

        If e.KeyChar = " " And player.IntersectsWith(door) And leveron = True Then
            win = True

            level3.Show()
            Me.Close()
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = False Then
            leveron = True
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = True Then
            leveron = False
        End If

    End Sub

    Private Sub level4_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If win = False Then
            Form2.Show()
        End If
    End Sub
End Class