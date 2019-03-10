using JILK.enums;
using System.Collections.Generic;

namespace JILK.Primitives
{
    public static class BorderDesign
    {
        private static readonly Dictionary<BorderPosition, char> SimpleBorder = new Dictionary<BorderPosition, char>()
            {
                { BorderPosition.Vertical, '│' },
                { BorderPosition.Horizontal, '─' },
                {BorderPosition.LeftUpper, '┌' },
                {BorderPosition.RightUpper, '┐' },
                {BorderPosition.LeftLower, '└' },
                {BorderPosition.RightLower, '┘' },
            };
        private static readonly Dictionary<BorderPosition, char> BoldBorder = new Dictionary<BorderPosition, char>()
            {
                { BorderPosition.Vertical, '┃' },
                { BorderPosition.Horizontal, '━' },
                {BorderPosition.LeftUpper, '┏' },
                {BorderPosition.RightUpper, '┓' },
                {BorderPosition.LeftLower, '┗' },
                {BorderPosition.RightLower, '┛' },
            };
        private static readonly Dictionary<BorderPosition, char> ExtraBoldBorder = new Dictionary<BorderPosition, char>()
            {
                { BorderPosition.Vertical, '█' },
                { BorderPosition.Horizontal, '█' },
                {BorderPosition.LeftUpper, '█' },
                {BorderPosition.RightUpper, '█' },
                {BorderPosition.LeftLower, '█' },
                {BorderPosition.RightLower, '█' },
            };
        private static readonly Dictionary<BorderPosition, char> DashlineBorder = new Dictionary<BorderPosition, char>()
            {
                { BorderPosition.Vertical, '┊' },
                { BorderPosition.Horizontal, '╌' },
                {BorderPosition.LeftUpper, '┌' },
                {BorderPosition.RightUpper, '┐' },
                {BorderPosition.LeftLower, '└' },
                {BorderPosition.RightLower, '┘' },
            };
        private static readonly Dictionary<BorderPosition, char> DoubleBorder = new Dictionary<BorderPosition, char>()
        {
                { BorderPosition.Vertical, '║' },
                { BorderPosition.Horizontal, '═' },
                { BorderPosition.LeftUpper, '╔' },
                { BorderPosition.RightUpper, '╗' },
                { BorderPosition.LeftLower, '╚' },
                { BorderPosition.RightLower, '╝' },
            };
        private static readonly Dictionary<BorderPosition, char> RoundedBorder = new Dictionary<BorderPosition, char>()
        {
                { BorderPosition.Vertical, '│' },
                { BorderPosition.Horizontal, '─' },
                { BorderPosition.LeftUpper, '╭' },
                { BorderPosition.RightUpper, '╮' },
                { BorderPosition.LeftLower, '╰' },
                { BorderPosition.RightLower, '╯' },
            };

        public static char GetBorderChar(BorderStyle style, BorderPosition position)
        {
            // TODO: Replace this switch with dictionary
            switch (style)
            {
                case (BorderStyle.Simple):
                    return SimpleBorder[position];
                case (BorderStyle.Bold):
                    return BoldBorder[position];
                case (BorderStyle.ExtraBold):
                    return ExtraBoldBorder[position];
                case (BorderStyle.Dashline):
                    return DashlineBorder[position];
                case (BorderStyle.Double):
                    return DoubleBorder[position];
                case (BorderStyle.Rounded):
                    return RoundedBorder[position];
                case (BorderStyle.None):
                    return ' ';
            }
            return SimpleBorder[position];
        }
    }
}
