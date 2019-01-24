using System.Collections.Generic;
using System.Linq;
using DemoParser.Models;

namespace DemoServer.Models
{
    public class DemoDto
    {
        public DemoDto()
        {
            Assets = new List<DemoAssetDto>();
            Walkthroughs = new List<DemoWalkthroughDto>();
        }

        public string Slug { get; set; }
        public string SourceCode { get; set; }
        public bool NonInteractive { get; set; }
        public string Hash { get; set; }
        public int UsingsLastLine { get; set; }
        public string Title { get; set; }
        public string DescriptionHtml { get; set; }
        public List<DemoAssetDto> Assets { get; set; }
        public List<DemoWalkthroughDto> Walkthroughs { get; set; }
        public string StudioUrl { get; set; }

        public static DemoDto FromModel(Demo demo)
        {
            var walkthroughDtos = demo.Walkthroughs.Select(DemoWalkthroughDto.FromModel).ToList();
            var assetDtos = demo.Assets.Select(DemoAssetDto.FromModel).ToList();

            return new DemoDto
            {
                Slug = demo.Slug,
                SourceCode = demo.SourceCode,
                NonInteractive = demo.NonInteractive,
                Hash = demo.Hash,
                UsingsLastLine = demo.UsingsLastLine,
                Title = demo.Title,
                DescriptionHtml = demo.DescriptionHtml,
                Walkthroughs = walkthroughDtos,
                Assets = assetDtos
            };
        }
    }

    public class DemoAssetDto
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public static DemoAssetDto FromModel(DemoAsset model)
        {
            return new DemoAssetDto
            {
                Title = model.Title,
                Url = model.Url,
                Type = model.Type.ToString()
            };
        }
    }

    public class DemoWalkthroughDto
    {
        public DemoWalkthroughDto()
        {
            Assets = new List<DemoAssetDto>();
        }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string DescriptionHtml { get; set; }
        public LinesRange Lines { get; set; }
        public List<DemoAssetDto> Assets { get; set; }
        public DemoLink DemoLink { get; set; }

        public static DemoWalkthroughDto FromModel(DemoWalkthrough model)
        {
            var assetDtos = model.Assets.Select(DemoAssetDto.FromModel).ToList();

            return new DemoWalkthroughDto
            {
                DescriptionHtml = model.DescriptionHtml,
                Slug = model.Slug,
                Title = model.Title,
                Lines = model.Lines,
                Assets = assetDtos,
                DemoLink = model.DemoLink
            };
        }
    }
}
