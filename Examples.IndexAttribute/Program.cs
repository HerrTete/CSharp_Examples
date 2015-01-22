using System;

namespace Examples.IndexAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            var wurstBrotBelag = new WurstBrotBelag
            {
                Brötchen = "Sesambrötchen",
                Schmiermittel = "Lätta",
                Wurst = "Serano Schinken",
                Käse = "Emmentaler",
                Topping = "Gurke"
            };

            for (int i = 0; i < 4; i++)
            {
                var valueWithIndex = GetValueWithIndex(wurstBrotBelag, i);
                Console.WriteLine(valueWithIndex);
            }

            Console.Read();
        }

        private static object GetValueWithIndex(object instance, int i)
        {
            object retVal = null;
            var properties = instance.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var reihenfolgeAttribute = propertyInfo.GetCustomAttributes(typeof(IndexAttribute), true);
                foreach (var attribute in reihenfolgeAttribute)
                {
                    var reihenfolge = attribute as IndexAttribute;
                    if (reihenfolge != null)
                    {
                        if (reihenfolge.Index == i)
                        {
                            retVal = propertyInfo.GetValue(instance);
                        }
                    }
                }
            }
            return retVal;
        }
    }

    class WurstBrotBelag
    {
        [Index(3)]
        public string Käse { get; set; }
        [Index(1)]
        public string Schmiermittel { get; set; }
        [Index(0)]
        public string Brötchen { get; set; }
        [Index(2)]
        public string Wurst { get; set; }
        [Index(4)]
        public string Topping { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    class IndexAttribute : Attribute
    {
        public int Index { get; set; }

        public IndexAttribute(int index)
        {
            Index = index;
        }
    }
}
