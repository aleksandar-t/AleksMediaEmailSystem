using Newtonsoft.Json;

namespace AleksMediaEmailSystem.EmailWorker.Helpers
{
    public class TemplateRenderer
    {

        // Reads template file from disk and processes placeholders
        public string RenderTemplate(string templateName, string MarketingDataJson)
        {
            // Load the template file
            var templatePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Common", "Templates"), $"{templateName}");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template file '{templatePath}' not found.");
            }

            Dictionary<string, string> placeholders = JsonConvert.DeserializeObject<Dictionary<string, string>>(MarketingDataJson); ;

            var templateContent = File.ReadAllText(templatePath);

            // Replace placeholders with values
            foreach (var placeholder in placeholders)
            {
                templateContent = templateContent.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return templateContent;
        }
    }
}
