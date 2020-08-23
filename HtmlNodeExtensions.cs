using System.Collections.Generic;
using System.Linq;

namespace HtmlAgilityPack {
    public static class HtmlNodeExtensions {
        private const string CommentNode = "//comment()";
        private const string DocTypeNode = "<!DOCTYPE html>";
        private const string TextNode = "//text()";

        public static IList<HtmlNode> SelectNodesAsList(
            this HtmlNode node,
            string xpath) => node.SelectNodes(xpath)?.ToList() ?? new List<HtmlNode>(0);

        public static void TrimWhitespace(
            this HtmlNode document) {
            var textNodes = document.SelectNodesAsList(TextNode).Where(
                n => string.IsNullOrWhiteSpace(n.InnerHtml));

            foreach (var textNode in textNodes) {
                textNode.Remove();
            }

            var commentNodes = document.SelectNodesAsList(CommentNode).Where(
                n => n.InnerHtml != DocTypeNode);

            foreach (var commentNode in commentNodes) {
                commentNode.Remove();
            }
        }
    }
}