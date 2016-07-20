using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNotex.Cells
{
    public class MyNewNoteCell2 : ViewCell
    {
        public MyNewNoteCell2()
        {
            var photoStudentImage = new Image
            {
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
            };

            //ahora mapeamos loas objetos de as clases:
            photoStudentImage.SetBinding(Image.SourceProperty, "Student.PhotoFullPath");

            var studenteFullName = new Label
            {

                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            studenteFullName.SetBinding(Label.TextProperty, "Student.FullName");

            var noteLabel = new Label
            {
               
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 100,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.Center,
            };
            noteLabel.SetBinding(Label.TextProperty, "Note", stringFormat: "Difinitiva: {0:N2}");


            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = { photoStudentImage, studenteFullName, noteLabel }
            };
        }

    }
}
