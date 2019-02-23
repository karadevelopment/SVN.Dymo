using DYMO.Label.Framework;
using System.Collections.Generic;
using System.IO;

namespace SVN.Dymo
{
    public class LabelWriter
    {
        private const string WRITER_450 = "DYMO LabelWriter 450";
        private string TemplateUri { get; }

        public LabelWriter(string templateUri)
        {
            this.TemplateUri = templateUri;
        }

        public IEnumerable<string> GetKeys()
        {
            using (var stream = new FileStream(this.TemplateUri, FileMode.Open))
            {
                var label = Label.Open(stream);

                foreach (var objectName in label.ObjectNames)
                {
                    yield return objectName;
                }
            }
        }

        public void Print(params (string key, object value)[] fields)
        {
            using (var stream = new FileStream(this.TemplateUri, FileMode.Open))
            {
                var label = Label.Open(stream);

                foreach (var (key, value) in fields)
                {
                    label.SetObjectText(key, value.ToString());
                }

                label.Print(LabelWriter.WRITER_450);
            }
        }
    }
}