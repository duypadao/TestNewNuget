using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCreator.Lab.Model
{
    public class TableFormat
    {
        public int ColSpan { get; set; }
        public Align Align { get; set; }
    }

    public enum Align
    {
        Left,
        Center,
        Right,
    }
}
