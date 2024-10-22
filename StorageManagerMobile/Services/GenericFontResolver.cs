using PdfSharp.Maui.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Services
{
    public class GenericFontResolver : ICustomFontProvider
    {

        private static readonly EZFontResolver EZFontResolver_fontResolver = EZFontResolver.Get;

        public string DefaultFontName => "OpenSans-Regular.ttf";

        public string FontPath { get; set; }

        public GenericFontResolver()
        {
           // EZFontResolver_fontResolver.AddFont("OpenSans-Regular", PdfSharpCore.Drawing.XFontStyle.Regular, "OpenSans-Regular.ttf", true, true);
        }

        public byte[] GetFont(string faceName)
        {
            // Implementazione per caricare i font personalizzati
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), faceName);
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            else
            {
                throw new FileNotFoundException($"Il font {faceName} non è stato trovato.");
            }
        }

        public string ProvideFont(string familyName, bool isBold, bool isItalic)
        {
            if (familyName == "PlatformDefault")
            {
                familyName= DefaultFontName;
            }
            // Implementazione per fornire i font personalizzati
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Resources), familyName);

            if (File.Exists(fontPath))
            {
                return fontPath;
            }
            else
            {
                return EZFontResolver_fontResolver.DefaultFontName;
                throw new FileNotFoundException($"Il font {familyName} non è stato trovato.");
            }
        }
    }
    //public class GenericFontResolver : ICustomFontProvider
    //{
    //    public byte[] GetFont(string faceName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public string ProvideFont(string fontName, bool isItalic, bool isBold)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
