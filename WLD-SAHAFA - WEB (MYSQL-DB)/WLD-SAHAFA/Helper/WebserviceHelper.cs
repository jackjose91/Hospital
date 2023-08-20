using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLD_SAHAFA.Extension;
using WLD_SAHAFA.Models;

namespace WLD_SAHAFA.Helper
{
    public static class WebserviceHelper
    {
        public static string BuildHtml(IList<Counter> counters)
        {
            if (counters.IsNullOrEmpty())
                return string.Empty;
            var htmlData = new StringBuilder();

            foreach (var counter in counters)
            {
                htmlData.Append(@"<div class='grid-item'>
                <div class='token'>Token: " + counter.ReceiptNo + @"</div>
                <div class='counter'>Counter: " + counter.CounterNo + @"</div>
                </div>");
            }
            return htmlData.ToString();
        }
        public static Tuple<string, string> BuildSpeechText(IList<Counter> oldCounters, IList<Counter> counters)
        {
            if (counters.IsNullOrEmpty())
                return Tuple.Create(string.Empty, string.Empty);
            IList<Counter> speechCounters = new List<Counter>();
            speechCounters = counters;
            if (!oldCounters.IsNullOrEmpty())
            {
                speechCounters = counters.Where(p => !oldCounters.Any(p2 => p2.CounterNo == p.CounterNo && p2.ReceiptNo == p.ReceiptNo)).ToList();
            }
                
            return speechCounters.IsNullOrEmpty() ?
                Tuple.Create(string.Empty, string.Empty) :
                CreateSpeechText(speechCounters);
        }
        private static Tuple<string, string> CreateSpeechText(IList<Counter> counters)
        {
            var speechTextEnglsih = new StringBuilder();
            var speechTextArabic = new StringBuilder();

            foreach (var counter in counters)
            {
                speechTextEnglsih.Append("Token number. " + counter.ReceiptNo.ConvertNumberToWordsEnglish() + " Counter number. " + counter.CounterNo.ConvertNumberToWordsEnglish() + " ");
                speechTextArabic.Append("Token number. " + counter.ReceiptNo.ConvertNumberToWordsArabic() + " Counter number. " + counter.CounterNo.ConvertNumberToWordsArabic() + " ");
            }
            return Tuple.Create(speechTextEnglsih.ToString(), speechTextArabic.ToString());
        }
    }
}