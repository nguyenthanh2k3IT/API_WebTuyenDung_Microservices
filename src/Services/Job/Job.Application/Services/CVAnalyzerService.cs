using Job.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Job.Application.Services
{
    public class CVAnalyzerService
    {
        private readonly KeywordExtractorService _keywordExtractor;

        public CVAnalyzerService()
        {
            _keywordExtractor = new KeywordExtractorService();
        }
        static string NormalizeText(string text)
        {
            text = text.ToLower(); // Chuyển về chữ thường
            text = Regex.Replace(text, @"\s+", " ").Trim(); // Xóa khoảng trắng thừa
            return text;
        }
        public CVAnalysisRequest AnalyzeCV(string cvText, string jobDescription)
        {
            string CV = NormalizeText(cvText);
            string JD = NormalizeText(jobDescription);
            // Trích xuất từ khóa từ CV và JD
            var cvKeywords = _keywordExtractor.ExtractKeywords(CV);
            var jdKeywords = _keywordExtractor.ExtractKeywords(JD);


          //  Tìm kỹ năng phù hợp và thiếu
           var matchingSkills = cvKeywords.Intersect(jdKeywords).ToList();
            var missingSkills = jdKeywords.Except(cvKeywords).ToList();

            // Tính điểm phù hợp
            float matchingScore = jdKeywords.Count == 0
                ? 0
                : (float)matchingSkills.Count / jdKeywords.Count * 100;

            return new CVAnalysisRequest
            {
                CVContent = cvText,
                JobDescription = jobDescription,
                MatchingScore = matchingScore,
                MatchingSkills = matchingSkills,
                MissingSkills = missingSkills
            };
        }


    }
}
