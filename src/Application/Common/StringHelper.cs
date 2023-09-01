namespace Application.Common;

public static class StringHelper
{
    public static void Reverse(ref string s)
    {
        var charArray = s.ToCharArray();
        Array.Reverse(charArray);
        s = new string(charArray);
    }
}