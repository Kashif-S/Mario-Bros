Public Module sprites
    Structure sprite
        Dim cellheight As Integer
        Dim cellwidth As Integer
        Dim cellcount As Integer
        Dim picture As Bitmap
        Dim startposition As Point
        Dim position As Point
        Dim speed As Point
        Dim startspeed As Point
        Dim MRectangle As Rectangle
        Dim celltop As Integer
        Dim facingRight As Boolean
        Dim onfloor As Boolean
        Dim state As Integer
    End Structure
    Public mario As sprite
    Public delay As Integer
    Public alternate As Boolean
    Public badguys(numbadguys) As sprite
    Public dino As sprite
    Public Const gravity As Integer = 1
    Public imageAttr As New System.Drawing.Imaging.ImageAttributes
    Public Const NORMAL As Integer = 0
    Public Const DEAD As Integer = 1
    Public Const REVIVE As Integer = 2
    Public Const numbadguys As Integer = 10
    Public numMovingBadGuys As Integer
    Public Const FLIP As Integer = 3
    Public reborn As Boolean
    Public startflying As Boolean
    Public Sub loadmario()
        mario.picture = My.Resources.sprite_strip
        mario.cellcount = 24
        mario.cellheight = mario.picture.Height / mario.cellcount
        mario.cellwidth = mario.picture.Width
        mario.startposition.X = 200
        mario.startposition.Y = 350
        mario.position.X = mario.startposition.X
        mario.position.Y = mario.startposition.Y
        mario.startspeed.Y = 16
        mario.startspeed.X = 5
        mario.speed.Y = 0
        mario.speed.X = 0
        mario.MRectangle.Width = mario.cellwidth
        mario.MRectangle.Height = mario.cellheight
    End Sub
    Public Sub loaddino()
        dino.picture = My.Resources.blablanadon_1_
        dino.cellcount = 3
        dino.cellheight = dino.picture.Width / dino.cellcount
        dino.cellwidth = dino.picture.Height
        dino.startposition.X = 200
        dino.startposition.Y = 350
        dino.position.X = dino.startposition.X
        dino.position.Y = dino.startposition.Y
        dino.startspeed.Y = 0
        dino.startspeed.X = 0
        dino.speed.Y = 0
        dino.speed.X = 0
        dino.MRectangle.Width = dino.cellwidth
        dino.MRectangle.Height = dino.cellheight
    End Sub
    Public Sub loadbadguys()
        Dim index As Integer
        For index = 0 To numbadguys
            badguys(index).picture = My.Resources.spinys
            badguys(index).cellcount = 19
            'cellheight and cellwidth have been switched
            badguys(index).cellheight = badguys(index).picture.Width / badguys(index).cellcount
            badguys(index).cellwidth = badguys(index).picture.Height

            badguys(index).MRectangle.Width = badguys(index).cellwidth
            badguys(index).MRectangle.Height = badguys(index).cellheight
            If alternate = False Then
                badguys(index).startposition.X = 68
                badguys(index).startposition.Y = 30
                badguys(index).speed.Y = 0
                badguys(index).speed.X = 5
                alternate = True
            Else
                badguys(index).startposition.X = 
                badguys(index).startposition.Y = 30
                badguys(index).speed.Y = 0
                badguys(index).speed.X = -5
                alternate = False
            End If


            badguys(index).position.X = badguys(index).startposition.X
            badguys(index).position.Y = badguys(index).startposition.Y

        Next
    End Sub
    Public Sub movemario()
        mario.speed = marioGetSpeed()
        mario.position.Y += mario.speed.Y
        mario.position.X += mario.speed.X
        If startflying = True Then
            If mario.state = REVIVE And mario.position.Y < 140 Then
                startflying = False
            End If

        End If
        If mario.state = REVIVE And mario.position.Y > 150 Then
            '  mario.position.Y = 5
            mario.speed.Y = 0
            startflying = True
        End If
        If mario.position.X > backdrop.width Then
            mario.position.X = 0
        End If
        If mario.position.X < 0 Then
            mario.position.X = backdrop.width
        End If
        mario.MRectangle.X = mario.position.X
        mario.MRectangle.Y = mario.position.Y
    End Sub

    Public Sub movedino()


        dino.position.Y += dino.speed.Y
        dino.position.X += dino.speed.X
        If reborn = False Then
            dino.MRectangle.X = mario.position.X - 4
            dino.MRectangle.Y = mario.position.Y - dino.cellheight + 3
        Else
            dino.speed.Y = -5
            dino.MRectangle.X = dino.position.X
            dino.MRectangle.Y = dino.position.Y
        End If
    End Sub
    Public Sub mariodraw()
        offG.DrawImage(mario.picture, mario.MRectangle, 0, mario.celltop, mario.cellwidth, mario.cellheight, System.Drawing.GraphicsUnit.Pixel, imageAttr)
    End Sub
    Public Sub badguydraw()
        Dim idx As Integer
        For idx = 0 To numMovingBadGuys
            offG.DrawImage(badguys(idx).picture, badguys(idx).MRectangle, badguys(idx).celltop, 0, badguys(idx).cellwidth, badguys(idx).cellheight, System.Drawing.GraphicsUnit.Pixel, imageAttr)
        Next
    End Sub
    Public Sub dinodraw()
        If mario.state = REVIVE Or reborn = True Then


            offG.DrawImage(dino.picture, dino.MRectangle, dino.celltop, 0, dino.cellwidth, dino.cellheight, System.Drawing.GraphicsUnit.Pixel, imageAttr)
        End If


    End Sub
    Public Function marioGetSpeed()
        mario.onfloor = False
        Dim nextStep As Point

        If startflying = False Then
            nextStep.Y = mario.speed.Y + gravity
        Else
            nextStep.Y = mario.speed.Y - gravity
        End If
        nextStep.X = mario.speed.X
        If mario.state = NORMAL Then
            mario.onfloor = False
            Dim index As Integer
            For index = 0 To 7
                If mario.position.X + mario.cellwidth > floors(index).left And mario.position.X < floors(index).right Then
                    If nextStep.Y > 0 Then
                        If mario.position.Y + mario.cellheight <= floors(index).top Then
                            If mario.position.Y + mario.cellheight + nextStep.Y <= floors(index).top Then

                            Else
                                nextStep.Y = floors(index).top - (mario.position.Y + mario.cellheight)
                                mario.onfloor = True
                            End If
                        End If
                    End If
                    If nextStep.Y <= 0 Then
                        If mario.position.Y >= floors(index).bottom Then
                            If mario.position.Y + nextStep.Y < floors(index).bottom Then
                                nextStep.Y = floors(index).bottom - mario.position.Y
                            End If
                        End If
                    End If
                End If

                If mario.position.Y + mario.cellheight > floors(index).top And mario.position.Y < floors(index).bottom Then
                    If nextStep.X > 0 Then
                        If mario.position.X + mario.cellwidth <= floors(index).left Then
                            If mario.position.X + mario.cellwidth + nextStep.X >= floors(index).left Then
                                nextStep.X = floors(index).left - (mario.position.X + mario.cellwidth)
                            End If
                        End If
                    End If
                    If nextStep.X <= 0 Then
                        If mario.position.X >= floors(index).right Then
                            If mario.position.X + nextStep.X < floors(index).right Then
                                nextStep.X = floors(index).right - mario.position.X
                            End If
                        End If
                    End If
                End If
            Next
        End If
        Return nextStep
    End Function
    Public Function badguysGetSpeed(ByVal idx As Integer)
        badguys(idx).onfloor = False
        Dim nextStep As Point
        Dim index As Integer
        If badguys(idx).position.Y < backdrop.height Then
            nextStep.Y = badguys(idx).speed.Y + gravity
            nextStep.X = badguys(idx).speed.X
        End If
        If badguys(idx).state <> DEAD Then
            For index = 0 To 7

                If badguys(idx).position.X + badguys(idx).cellwidth > floors(index).left And badguys(idx).position.X < floors(index).right Then
                    If nextStep.Y > 0 Then
                        If badguys(idx).position.Y + badguys(idx).cellheight <= floors(index).top Then
                            If badguys(idx).position.Y + badguys(idx).cellheight + nextStep.Y <= floors(index).top Then

                            Else
                                nextStep.Y = floors(index).top - (badguys(idx).position.Y + badguys(idx).cellheight)
                                badguys(idx).onfloor = True
                            End If
                        End If
                    End If
                End If

            Next
        End If
        Return nextStep
    End Function
    Public Function animateMario()
        Dim celltop As Integer
        celltop = mario.celltop
        celltop += mario.cellheight
        If mario.state = NORMAL Then
            If mario.speed.X > 0 Then
                mario.facingRight = True
                If mario.onfloor = True Then
                    If celltop < mario.cellheight * 16 Or celltop > mario.cellheight * 23 Then
                        celltop = mario.cellheight * 16
                    End If
                ElseIf mario.speed.Y < 0 Then
                    celltop = mario.cellheight * 5
                ElseIf mario.speed.Y > 0 Then
                    celltop = mario.cellheight * 4
                End If
            End If

                If mario.speed.X < 0 Then
                    mario.facingRight = False
                    If mario.onfloor = True Then
                    If celltop < mario.cellheight * 7 Or celltop > mario.cellheight * 13 Then
                        celltop = mario.cellheight * 7
                    End If
                ElseIf mario.speed.Y < 0 Then
                    celltop = mario.cellheight * 15
                ElseIf mario.speed.Y > 0 Then
                    celltop = mario.cellheight * 14
                End If
                End If

                If mario.speed.X = 0 Then
                    If mario.facingRight = True Then
                        If mario.onfloor = True Then
                        celltop = mario.cellheight * 23
                    ElseIf mario.speed.Y < 0 Then
                        celltop = mario.cellheight * 5
                    ElseIf mario.speed.Y > 0 Then
                        celltop = mario.cellheight * 4
                    End If
               

                    Else
                        If mario.onfloor = True Then
                        celltop = mario.cellheight * 13
                    ElseIf mario.speed.Y < 0 Then
                        celltop = mario.cellheight * 15
                    ElseIf mario.speed.Y > 0 Then
                        celltop = mario.cellheight * 14
                    End If
                End If
            End If


        ElseIf mario.state = DEAD Then
            If celltop > mario.cellheight * 3 Then
                celltop = mario.cellheight * 0
            End If
        ElseIf mario.state = REVIVE Then
            If celltop > mario.cellheight * 4 Then
                celltop = mario.cellheight * 4
            End If
        End If
        Return celltop
    End Function
    Public Sub movebadguys(ByVal numMoving As Integer)
        Dim index As Integer
        For index = 0 To numMoving
            badguys(index).speed = badguysGetSpeed(index)
            badguys(index).position.X += badguys(index).speed.X
            badguys(index).position.Y += badguys(index).speed.Y
            If badguys(index).position.X > backdrop.width Then
                badguys(index).position.X = 0
            End If

            If badguys(index).position.X < 0 Then
                badguys(index).position.X = backdrop.width
            End If
            If badguys(index).position.X < 0 And badguys(index).position.Y > 300 Then
                badguys(index).position.X = backdrop.width
                badguys(index).position.Y = 0
            End If
            If badguys(index).position.X < 0 And badguys(index).position.Y > 1 Then
                badguys(index).position.X = backdrop.width
                badguys(index).position.Y = 0
            End If


            badguys(index).MRectangle.X = badguys(index).position.X
            badguys(index).MRectangle.Y = badguys(index).position.Y

        Next
    End Sub
    Public Function animatebadguys(ByVal idx As Integer)
        Dim celltop As Integer
        celltop = badguys(idx).celltop
        celltop += badguys(idx).cellheight
        If badguys(idx).state = NORMAL Then
            If badguys(idx).speed.X < 0 Then
                badguys(idx).facingRight = False
                If celltop > badguys(idx).cellheight * 4 Then
                    celltop = 0
                End If
            End If

            If badguys(idx).speed.X > 0 Then
                badguys(idx).facingRight = True
                If celltop < badguys(idx).cellheight * 5 Or celltop > badguys(idx).cellheight * 9 Then
                    celltop = badguys(idx).cellheight * 5
                End If
            End If
        ElseIf badguys(idx).state = FLIP Then
            If badguys(idx).onfloor = False Then
                If celltop < badguys(idx).cellheight * 14 Or celltop > badguys(idx).cellheight * 18 Then
                    celltop = badguys(idx).cellheight * 14
                End If
            Else
                If celltop < badguys(idx).cellheight * 10 Or celltop > badguys(idx).cellheight * 13 Then
                    celltop = badguys(idx).cellheight * 10
                End If
            End If
        ElseIf badguys(idx).state = DEAD Then
            If celltop < badguys(idx).cellheight * 14 Or celltop > badguys(idx).cellheight * 18 Then
                celltop = badguys(idx).cellheight * 14
            End If
        End If
        Return celltop
    End Function
    Public Function animatedino()
        Dim celltop As Integer
        celltop = dino.celltop
        If delay = 3 Then
            celltop += dino.cellheight
            delay = 0
            If celltop > dino.cellheight * 2 Then
                celltop = dino.cellheight * 0
            End If
        End If
        Return celltop
    End Function
    Public Function touching(ByVal guy1 As sprite, ByVal guy2 As sprite)
        Dim rad1 As Double
        Dim rad2 As Double
        If guy1.cellwidth < guy1.cellheight Then
            rad1 = guy1.cellwidth / 2
        Else
            rad1 = guy1.cellheight / 2
        End If
        If guy2.cellwidth < guy2.cellheight Then
            rad2 = guy2.cellwidth / 2
        Else
            rad2 = guy2.cellheight / 2
        End If
        Dim a As Integer
        Dim b As Integer
        Dim c As Double
        a = guy1.position.Y - guy2.position.Y
        b = guy1.position.X - guy2.position.X
        c = Math.Sqrt(a * a + b * b)
        If c < rad1 + rad1 Then
            Return True
        End If
        Return False
    End Function
    Public Sub killmario()
        Dim index As Integer
        For index = 0 To numMovingBadGuys
            If mario.state = NORMAL Then
                If badguys(index).state = NORMAL Then
                    If touching(mario, badguys(index)) = True Then
                        mario.state = DEAD
                        mario.speed.X = 0
                        mario.speed.Y = -20
                    End If
                End If
            End If
        Next
    End Sub
    Public Sub revival()
        If mario.state = DEAD And mario.position.Y >= backdrop.height Then
            mario.state = REVIVE
            dino.position = mario.position
            mario.position.Y = -mario.cellheight
            mario.position.X = backdrop.width / 2 - mario.cellwidth / 2
            mario.speed.Y = 0
        End If
    End Sub
    Public Sub killbadguys()
        Dim shiftedmario As sprite
        shiftedmario = mario
        shiftedmario.position.Y -= 40
        Dim index As Integer
        Dim idx As Integer
        For index = 0 To numMovingBadGuys
            For idx = 0 To numMovingBadGuys
                If badguys(index).onfloor = True And badguys(index).state <> DEAD Then

                    If (mario.state = NORMAL And touching(shiftedmario, badguys(index))) Or (badguys(idx).state = DEAD And touching(badguys(idx), badguys(index))) Then
                        If idx <> index Then
                            badguys(index).state = FLIP
                            badguys(index).speed.Y = -20
                            badguys(index).speed.X = 0
                        End If
                    End If
                End If
                If mario.state = NORMAL And badguys(index).state = FLIP Then
                    If touching(mario, badguys(index)) Then
                        badguys(index).state = DEAD
                        badguys(index).speed.Y = -20
                        badguys(index).speed.X = mario.speed.X
                    End If
                End If
            Next
        Next
    End Sub
End Module


