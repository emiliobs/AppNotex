using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace AppNotex.Cells
{
    public class MySubjectCell : ViewCell
    {
        //objetos:
        public MySubjectCell()
        {
            var photoTeacherImage = new Image
            {
                HeightRequest = 150,
                WidthRequest = 150,
            };

            photoTeacherImage.SetBinding(Image.SourceProperty, "teacher.PhotoFullPath");

            var subjectNameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            subjectNameLabel.SetBinding(Label.TextProperty, "Description");

            var teacherFullNameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            teacherFullNameLabel.SetBinding(Label.TextProperty, "teacher.FullName");

            var noteLabel = new Label
            {
                 HorizontalOptions = LayoutOptions.FillAndExpand,
                 FontSize = 20,
                 FontAttributes = FontAttributes.Bold,
                 BackgroundColor = Color.White,
                 TextColor = Color.Red,
            };

            noteLabel.SetBinding(Label.TextProperty, "Note", stringFormat: "Difinitiva: {0:N2}");

            var left = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { photoTeacherImage, },
            };

            var right = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { subjectNameLabel, teacherFullNameLabel, noteLabel, },
            };

             View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = { left, right, },
            };
        }
    }
}
