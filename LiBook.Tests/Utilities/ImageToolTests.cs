using System.Drawing;
using LiBook.Utilities.Images;
using Xunit;

namespace LiBook.Tests.Utilities
{
    public class ImageToolTests
    {
        private readonly Image _wideImage;
        private readonly Image _hightImage;
        private readonly Image _squareImage;

        public ImageToolTests()
        {
            // Arrange
            _wideImage = new Bitmap(600, 500);
            _hightImage = new Bitmap(500, 600);
            _squareImage = new Bitmap(500, 500);

        }

        [Fact]
        public void ResizeTest()
        {
            // Act
            var zoomedOut = ImageTool.Resize(_wideImage, 100, 100);
            var zoomedIn = ImageTool.Resize(_wideImage, 700, 700);
            var noZoom = ImageTool.Resize(_wideImage, 600, 500);

            // Assert
            Assert.Equal(100, zoomedOut.Width);
            Assert.Equal(100, zoomedOut.Height);

            Assert.Equal(700, zoomedIn.Width);
            Assert.Equal(700, zoomedIn.Height);

            Assert.Equal(600, noZoom.Width);
            Assert.Equal(500, noZoom.Height);
        }

        [Fact]
        public void CropMaxSquareTest()
        {
            // Act 
            var img1 = ImageTool.CropMaxSquare(_wideImage);
            var img2 = ImageTool.CropMaxSquare(_hightImage);
            var img3 = ImageTool.CropMaxSquare(_squareImage);

            // Assert
            Assert.Equal(500, img1.Width);
            Assert.Equal(500, img1.Height);

            Assert.Equal(500, img2.Width);
            Assert.Equal(500, img2.Height);

            Assert.Equal(500, img3.Width);
            Assert.Equal(500, img3.Height);
        }
    }
}
