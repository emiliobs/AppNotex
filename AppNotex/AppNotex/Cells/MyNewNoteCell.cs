using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNotex.Cells
{
   public class MyNewNoteCell : ViewCell
    {

        public MyNewNoteCell()
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

            var noteEntry = new Entry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 70,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.Center,
            };
            noteEntry.SetBinding(Entry.TextProperty, "Note");


            View = new StackLayout
            {
                Orientation =StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = {photoStudentImage, studenteFullName, noteEntry }
            };

        }
    }
}
