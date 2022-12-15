Public Class form1
    Dim canvas As Bitmap
    Dim canvaspen, formpen As Graphics
    Dim PlayerX, PlayerY, platX(10), platY(10), obstX(10), obstY(10), frame, leverx, levery As Integer
    Public Level As Integer

    Dim left, right, obstleft, leveron, platup, overide As Boolean

    Dim jumping, win As Boolean 'True if mid-jump, false if not jumping
    Dim jumpmove As Integer 'Used to control jump speed
    Dim jumpheight As Integer 'max height of jump
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form2.level(2) = True
        PlayerY = GH() - 50
        PlayerX = 75
        Level = 1
        canvas = New Bitmap(Me.Width, Me.Height)
        canvaspen = Graphics.FromImage(canvas)
        formpen = Me.CreateGraphics
        leveron = False


        jumpheight = 15

        platY(1) = GH() - 125
        platX(1) = 200

        platY(2) = 650
        platX(2) = 910

        platX(3) = 550
        platY(3) = GH() - 75
        platup = True

        obstX(1) = 500
        obstY(1) = GH() - 50

        leverx = platX(1) + 75 - (25 / 2)
        levery = platY(1) - 25

        win = False

        Timer1.Start()
    End Sub
    Function GH()
        Return Me.ClientSize.Height
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim plat3air As New Rectangle(platX(3), platY(3) - 25, 175, 50)

        If jumping = False Then
            Label2.Text = "False" & vbCrLf & jumpmove & vbCrLf & platup
        Else
            Label2.Text = "True" & vbCrLf & jumpmove & vbCrLf & platup
        End If

        If jumping = True Then
            PlayerY = PlayerY - jumpmove
            jumpmove = jumpmove - 1
        End If

        If platup = True Then
            platY(3) = platY(3) - 1
            If platY(3) <= platY(2) Then
                platup = False
            End If
        Else
            platY(3) = platY(3) + 1
            If platY(3) >= GH() - 75 Then
                platup = True
            End If
            If player.IntersectsWith(plat3air) Then
                jumping = False
                If overide = True Then
                    PlayerY = PlayerY - jumpmove
                    jumpmove = jumpmove - 1
                Else
                    PlayerY = platY(3) - 24
                End If
            End If
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

        If obstleft = True Then
            obstX(1) = obstX(1) - 5
            If obstX(1) <= 400 Then
                obstleft = False
            End If
        Else
            obstX(1) = obstX(1) + 5
            If obstX(1) >= 900 Then
                obstleft = True
            End If
        End If

        collision()

        drawframe()
    End Sub

    Sub collision()
        Dim plat As New Rectangle(platX(1), platY(1), 150, 25)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)
        Dim saw As New Rectangle(obstX(1), obstY(1), 35, 35)
        Dim plat2 As New Rectangle(platX(2), platY(2), 150, 25)
        Dim plat3 As New Rectangle(platX(3), platY(3), 175, 25)

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
            jumpmove = 0
            jumping = False
            If PlayerY > platY(3) Then
                PlayerY = platY(3) + 26
            Else
                PlayerY = platY(3) - 25
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

    End Sub
    Sub drawframe()

        canvaspen.Clear(Color.Black)
        If leveron = False Then
            canvaspen.DrawImage(My.Resources.Door, platX(2) + 50, platY(2) - 70, 50, 75)
        Else
            canvaspen.DrawImage(My.Resources.door2, platX(2) + 50, platY(2) - 70, 70, 75)
        End If

        If leveron = True Then
            canvaspen.DrawImage(My.Resources.Lever2, leverx, levery, 25, 25)
        Else
            canvaspen.DrawImage(My.Resources.Lever, leverx, levery, 25, 25)
        End If

        canvaspen.FillRectangle(Brushes.Blue, platX(3), platY(3), 175, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(1), platY(1), 150, 25)
        canvaspen.FillRectangle(Brushes.White, PlayerX, PlayerY, 25, 25)
        canvaspen.FillRectangle(Brushes.Blue, platX(2), platY(2), 150, 25)
        canvaspen.DrawImage(My.Resources.skull, 0, 0, 50, 50)

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

    Function GW()
        Return Me.ClientSize.Width
    End Function

    Private Sub form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim Door As New Rectangle(platX(2) + 50, platY(2) - 70, 50, 75)
        Dim lever As New Rectangle(leverx - 25, levery - 25, 50, 50)
        Dim player As New Rectangle(PlayerX, PlayerY, 25, 25)

        If e.KeyChar = "w" And jumping = False Then
            jumping = True
            jumpmove = jumpheight
            overide = True
            My.Computer.Audio.Play(My.Resources.Jump, AudioPlayMode.Background)
        End If

        If e.KeyChar = " " And player.IntersectsWith(Door) And leveron = True Then
            win = True
            level2.Show()
            Me.Close()
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = False Then
            leveron = True
        ElseIf e.KeyChar = " " And player.IntersectsWith(lever) And leveron = True Then
            leveron = False
        End If


    End Sub

    Private Sub form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.A Then
            left = True
        End If
        If e.KeyValue = Keys.D Then
            right = True
        End If
    End Sub

    Private Sub form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.A Then
            left = False
        End If
        If e.KeyValue = Keys.D Then
            right = False
        End If
    End Sub

    Private Sub form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If win = False Then
            Form2.Show()
        End If

    End Sub
End Class
