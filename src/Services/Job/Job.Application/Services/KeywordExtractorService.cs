using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Job.Application.Services
{
    public class KeywordExtractorService
    {
        private readonly MLContext _mlContext;
       
        public KeywordExtractorService()
        {
            _mlContext = new MLContext();
        }

       
        public List<string> ExtractKeywords(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();

            // Bỏ qua các từ thông dụng (stop words)
            var stopWords = new HashSet<string> {   "a", "about", "above", "after", "again", "against", "all", "am", "an", "and", "any", "are", "aren't",
    "as", "at", "be", "because", "been", "before", "being", "below", "between", "both", "but", "by",
    "can't", "cannot", "could", "couldn't", "did", "didn't", "do", "does", "doesn't", "doing", "don't",
    "down", "during", "each", "few", "for", "from", "further", "had", "hadn't", "has", "hasn't", "have",
    "haven't", "having", "he", "he'd", "he'll", "he's", "her", "here", "here's", "hers", "herself", "him",
    "himself", "his", "how", "how's", "i", "i'd", "i'll", "i'm", "i've", "if", "in", "into", "is", "isn't",
    "it", "it's", "its", "itself", "let's", "me", "more", "most", "mustn't", "my", "myself", "no", "nor",
    "not", "of", "off", "on", "once", "only", "or", "other", "ought", "our", "ours", "ourselves", "out",
    "over", "own", "same", "shan't", "she", "she'd", "she'll", "she's", "should", "shouldn't", "so", "some",
    "such", "than", "that", "that's", "the", "their", "theirs", "them", "themselves", "then", "there",
    "there's", "these", "they", "they'd", "they'll", "they're", "they've", "this", "those", "through", "to",
    "too", "under", "until", "up", "very", "was", "wasn't", "we", "we'd", "we'll", "we're", "we've", "were",
    "weren't", "what", "what's", "when", "when's", "where", "where's", "which", "while", "who", "who's",
    "whom", "why", "why's", "with", "won't", "would", "wouldn't", "you", "you'd", "you'll", "you're",
    "you've", "your", "yours", "yourself", "yourselves","looking","job","developer","experience","\n","\b","knowledge","required", "skilled"};

          
            // Chuyển văn bản thành dataset
            var data = new List<TextData> { new TextData { Text = text } };
            var dataView = _mlContext.Data.LoadFromEnumerable(data);
          
            // Cấu hình pipeline cho Text Normalization
            var pipeline = _mlContext.Transforms.Text.NormalizeText("NormalizedText", "Text")
                .Append(_mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
                .Append(_mlContext.Transforms.Text.RemoveDefaultStopWords("CleanedTokens", "Tokens"))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Keywords", "CleanedTokens"));

            // Huấn luyện mô hình
            var model = pipeline.Fit(dataView);
            var transformedData = model.Transform(dataView);

            // Lấy danh sách từ khóa
            var tokens = text.ToLower()
                .Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(words =>! stopWords.Contains(words)).Distinct().ToList();

            return tokens;
        }

        // Class để xử lý văn bản đầu vào
        private class TextData
        {
            public string Text { get; set; }
        }

        // Class kết quả sau khi xử lý NLP
        private class TransformedTextData
        {
            public string NormalizedText { get; set; }
            public string[] Tokens { get; set; }
            public string[] CleanedTokens { get; set; }
        }

    }
}
