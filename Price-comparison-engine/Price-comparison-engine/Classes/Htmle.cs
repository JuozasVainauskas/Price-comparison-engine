namespace Price_comparison_engine.Classes
{
    public class Htmle
    {
        public struct HtmlE
        {
            public readonly string htmlEnd;

            public HtmlE(int index, string htmlEnd)
            {
                if (index == 0)
                {
                    this.htmlEnd = htmlEnd;
                }
                else
                {
                    this.htmlEnd = "";
                }

            }

        }
    }
}