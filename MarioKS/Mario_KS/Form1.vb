Public Class Mainform
    Public counter As Integer

    Public music As New System.Media.SoundPlayer("Rookie and Popple.wav")

    Private Sub Mainform_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If mario.state = NORMAL Or mario.state = REVIVE Then
            If mario.state = REVIVE Then
                reborn = True
                startflying = False
                mario.state = NORMAL
            ElseIf mario.state = NORMAL Then
                If e.KeyCode = Keys.Right Then
                    mario.speed.X = mario.startspeed.X
                End If
                If e.KeyCode = Keys.Left Then
                    mario.speed.X = -mario.startspeed.X
                End If
                If e.KeyCode = Keys.Up Then
                    If mario.onfloor = True Then
                        mario.speed.Y = -mario.startspeed.Y
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Mainform_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Left Then
            mario.speed.X = 0
        End If
        If e.KeyCode = Keys.Right Then
            mario.speed.X = 0
        End If
    End Sub

    Private Sub Mainform_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadmario()
        loadbadguys()
        loaddino()
        backgroundset()
        back.Height = backdrop.height
        back.Width = backdrop.width
        music.Load()
        music.PlayLooping()

        back.Left = backdrop.position.X
        back.Top = backdrop.position.Y
        Me.Height = backdrop.height + 30
        Me.Width = backdrop.width
        OffScreenSet()
        floorset()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim index As Integer
        movemario()
        screendraw()
        mario.celltop = animateMario()
        dino.celltop = animatedino()
        killmario()
        revival()
        movedino()

        killbadguys()
        If dino.position.Y < 0 Then

            reborn = False


        End If
        If reborn = True Then
            dino.speed.Y = -5
        End If
        delay += 1

        For index = 0 To numMovingBadGuys
            badguys(index).celltop = animatebadguys(index)
        Next
        counter += 1
        numMovingBadGuys = counter / 20
        If numMovingBadGuys > numbadguys Then
            numMovingBadGuys = numbadguys
        End If
        movebadguys(numMovingBadGuys)
    End Sub
    Private Sub screendraw()
        g = back.CreateGraphics
        offG = Graphics.FromImage(ImageOffScreen)
        BackgroundDraw()
        mariodraw()
        badguydraw()
        dinodraw()
        g.DrawImage(ImageOffScreen, 0, 0)
        g.Dispose()
        offG.Dispose()
    End Sub

End Class
