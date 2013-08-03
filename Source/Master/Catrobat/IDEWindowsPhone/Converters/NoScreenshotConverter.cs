﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Catrobat.Core.Storage;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class NoScreenshotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value;
            }
            else
            {
                try
                {
                    var image = new BitmapImage();
                    using (var loader = ResourceLoader.CreateResourceLoader())
                    {
                        var stream = loader.OpenResourceStream(ResourceScope.IdePhone, "Content/Images/Screenshot/NoScreenshot.png");
                        image.SetSource(stream);
                    }
                    return image;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // not used
            return null;
        }
    }
}