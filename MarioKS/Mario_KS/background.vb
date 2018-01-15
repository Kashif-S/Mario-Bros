Module background
    Structure background
        Dim picture As Bitmap
        Dim position As Point
        Dim height As Integer
        Dim width As Integer
    End Structure
    Structure floor
        Dim top As Integer
        Dim bottom As Integer
        Dim left As Integer
        Dim right As Integer
    End Structure
    Public floors(7) As floor
    Public backdrop As background
    Public g As Graphics
    Public offG As Graphics
    Public ImageOffScreen As Image
    Public lowercolor As Color = Color.FromArgb(0, 0, 0)
    Public uppercolor As Color = Color.FromArgb(0, 0, 0)
    Public Sub backgroundset()
        backdrop.position.X = 0
        backdrop.position.Y = 0
        backdrop.picture = My.Resources.bckgrnd2
        backdrop.width = backdrop.picture.Width
        backdrop.height = backdrop.picture.Height
    End Sub
    Public Sub OffScreenSet()
        ImageOffScreen = backdrop.picture.Clone
        imageAttr.SetColorKey(lowercolor, uppercolor, System.Drawing.Imaging.ColorAdjustType.Default)
    End Sub
    Public Sub BackgroundDraw()
        offG.DrawImage(backdrop.picture, 0, 0)
    End Sub
    Public Sub floorset()
        floors(0).top = 415
        floors(0).bottom = 450
        floors(0).left = -50
        floors(0).right = 700

        floors(1).top = 320
        floors(1).bottom = 335
        floors(1).left = -50
        floors(1).right = 240

        floors(2).top = 320
        floors(2).bottom = 335
        floors(2).left = 405
        floors(2).right = 700

        floors(3).top = 218
        floors(3).bottom = 232
        floors(3).left = -50
        floors(3).right = 97

        floors(4).top = 218
        floors(4).bottom = 232
        floors(4).left = 548
        floors(4).right = 700

        floors(5).top = 204
        floors(5).bottom = 218
        floors(5).left = 165
        floors(5).right = 475

        floors(6).top = 96
        floors(6).bottom = 110
        floors(6).left = -50
        floors(6).right = 265

        floors(7).top = 96
        floors(7).bottom = 110
        floors(7).left = 380
        floors(7).right = 700
    End Sub
End Module