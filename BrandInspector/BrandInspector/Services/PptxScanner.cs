using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing; 
using BrandInspector.Models;
using PShape = DocumentFormat.OpenXml.Presentation.Shape;

namespace BrandInspector.Services
{
    public class PptxScanner
    {
        public List<ScanResult> ScanFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found", path);

            var results = new List<ScanResult>();

            using (var doc = PresentationDocument.Open(path, false))
            {
                int slideNo = 0;

                foreach (var slide in doc.PresentationPart.SlideParts)
                {
                    slideNo++;

                    foreach (var shape in slide.Slide.Descendants<PShape>())
                    {
                        var textBody = shape.TextBody;
                        if (textBody == null) continue;

                        string text = string.Empty;
                        string font = string.Empty;
                        string color = string.Empty;
                        double size = 0;

                        foreach (var paragraph in textBody.Elements<Paragraph>())
                        {
                            var defaultProps = paragraph.ParagraphProperties?
                                .GetFirstChild<DefaultRunProperties>();

                            foreach (var run in paragraph.Elements<Run>())
                            {
                                text += run.Text?.Text ?? string.Empty;

                                var props = run.RunProperties;

                                if (props == null && defaultProps != null)
                                {
                                    var latinDefault = defaultProps.GetFirstChild<LatinFont>();
                                    if (latinDefault?.Typeface?.Value != null)
                                        font = latinDefault.Typeface.Value.Trim();

                                    if (defaultProps.FontSize != null)
                                        size = Math.Round((double)defaultProps.FontSize / 100, 2);

                                    var fillDefault = defaultProps.GetFirstChild<SolidFill>();
                                    if (fillDefault != null)
                                    {
                                        var rgb = fillDefault.GetFirstChild<RgbColorModelHex>();
                                        if (rgb?.Val?.Value != null)
                                            color = "#" + rgb.Val.Value.ToUpperInvariant();
                                    }

                                    continue; 
                                }

                                if (props != null)
                                {
                                    // FONT
                                    var latin = props.GetFirstChild<LatinFont>();
                                    if (latin?.Typeface?.Value != null && !string.IsNullOrWhiteSpace(latin.Typeface.Value))
                                        font = latin.Typeface.Value.Trim();

                                    // SIZE
                                    if (props.FontSize != null)
                                        size = Math.Round((double)props.FontSize / 100, 2);

                                    // COLOR
                                    var fill = props.GetFirstChild<SolidFill>();
                                    if (fill != null)
                                    {
                                        var rgb = fill.GetFirstChild<RgbColorModelHex>();
                                        if (rgb?.Val?.Value != null && !string.IsNullOrWhiteSpace(rgb.Val.Value))
                                            color = "#" + rgb.Val.Value.ToUpperInvariant();
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            results.Add(new ScanResult
                            {
                                SlideNumber = slideNo,
                                ShapeType = shape.NonVisualShapeProperties?
                                    .NonVisualDrawingProperties?.Name?.Value ?? "Shape",
                                SampleText = text.Length > 60 ? text.Substring(0, 60) + "..." : text,
                                FontFamily = font,
                                FontSize = size,
                                Color = color
                            });
                        }
                    }
                }
            }

            return results;
        }

        public void CheckCompliance(List<ScanResult> results, BrandSettings settings, string type)
        {
            foreach (var r in results)
            {
                bool pass = true;
                string reason = string.Empty;

                switch (type)
                {
                    case "Fonts":
                        pass = settings.Fonts.Any(f =>
                            string.Equals(f.Trim(), r.FontFamily?.Trim(), StringComparison.OrdinalIgnoreCase));
                        reason = pass ? "OK" : $"Font '{r.FontFamily}' not allowed";
                        break;

                    case "Colors":
                        pass = settings.Colors.Any(c =>
                            string.Equals(c.Trim().ToUpperInvariant(), (r.Color ?? string.Empty).Trim().ToUpperInvariant()));
                        reason = pass ? "OK" : $"Color '{r.Color}' not allowed";
                        break;

                    case "Sizes":
                        pass = settings.Sizes.Any(s => Math.Abs(s - r.FontSize) <= 1);
                        reason = pass ? "OK" : $"Size '{r.FontSize}' not allowed";
                        break;
                }

                r.Compliance = pass ? "Pass" : $"Fail ({reason})";
            }
        }
    }
}
