using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser.Models;

namespace DemoParser.Utils.Json
{
    internal static class ConversionExtensions
    {
        public static Demo ToDemo(this JsonMetadata json)
        {
            var demo = new Demo();
            demo.Title = json.Title;
            demo.DescriptionHtml = ConvertToDescription(json.DescriptionLines);

            demo.Assets = json.Assets.Select(ToModel).ToList();
            demo.Walkthroughs = json.Walkthroughs.Select(ToModel).ToList();
            demo.Slug = json.Slug.ToLower();

            CopyOptionalFields(from: json, to: demo);

            return demo;
        }

        private static void CopyOptionalFields(JsonMetadata from, Demo to)
        {
            to.NonInteractive = from.NonInteractive;
            to.StudioLinkToMediaDatabase = from.StudioLinkToMediaDatabase;
            to.StudioLinkToIndexList = from.StudioLinkToIndexList;
            to.ConferenceOnly = from.ConferenceOnly;
        }

        private static DemoWalkthrough ToModel(JsonDemoWalkthrough json)
        {
            var model = new DemoWalkthrough
            {
                Lines = json.Lines,
                Title = json.Title,
                Slug = json.Slug
            };

            model.DemoLink = ToModel(json.DemoLink);
            model.Assets = json.Assets.Select(ToModel).ToList();
            model.DescriptionHtml = ConvertToDescription(json.DescriptionLines);

            return model;
        }

        private static DemoAsset ToModel(JsonDemoAsset json)
        {
            return new DemoAsset
            {
                Title = json.Title,
                Type = json.Type,
                Url = json.Url
            };
        }

        private static string ConvertToDescription(IEnumerable<string> descriptionLines) =>
            descriptionLines == null ? null : string.Join(Environment.NewLine, descriptionLines);

        private static DemoLink ToModel(JsonDemoLink json)
        {
            if (json == null)
                return null;

            return new DemoLink
            {
                Url = json.Url,
                Title = json.Title
            };
        }
    }
}
