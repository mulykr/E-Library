using System.Drawing;

namespace LiBook.Utilities.Images
{
    public class ImageTool
    {
        public static Image Resize(Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }

            return newImage;
        }

        public static Image CropMaxSquare(Image image)
        {
            var width = image.Width > image.Height ? image.Height : image.Width;
            var height = width;

            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage))
                g.DrawImage(
                    image,
                    new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, width, height),
                    GraphicsUnit.Pixel
                );
            return newImage;
        }
    }
}
