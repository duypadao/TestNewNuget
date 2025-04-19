using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCreator.Lab.Extension
{
    public static class ContainerExtensions
    {
        public static void AlignCenterIf(this TextDescriptor descriptor, bool condition)
        {
            if (condition) descriptor.AlignCenter();
        }
    }

}
