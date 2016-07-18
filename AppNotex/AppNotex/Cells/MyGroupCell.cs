using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNotex.Cells
{
  public  class MyGroupCell:ViewCell
    {
        public MyGroupCell()
        {
            var descriotionLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 20,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
            };

            descriotionLabel.SetBinding(Label.TextProperty, "Description");

          
            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = { descriotionLabel, },
            };
        }
    }
}
