using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using System.Linq;
using Windows.Storage;

namespace Flip_Chess
{
    partial class MainPage
    {
        public int LoadStep()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Step"))
            {
                if (ApplicationData.Current.LocalSettings.Values["Step"] is int item)
                {
                    return item;
                }
            }

            return default;
        }

        public void SaveStep(int value)
        {
            ApplicationData.Current.LocalSettings.Values["Step"] = value;
        }

        public bool ReadRandom()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Random"))
            {
                if (ApplicationData.Current.LocalSettings.Values["Random"] is byte[] item)
                {
                    for (int i = 0; i < System.Math.Min(item.Length, this.Randoms.Length); i++)
                    {
                        this.Randoms[i].Type = (ChessType)(int)item[i];
                    }
                    return true;
                }
            }

            return false;
        }

        public bool WriteRandom()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Random"))
            {
                if (ApplicationData.Current.LocalSettings.Values["Random"] is byte[] item)
                {
                    if (item.Length == this.Randoms.Length)
                    {
                        for (int i = 0; i < this.Randoms.Length; i++)
                        {
                            item[i] = (byte)(int)this.Randoms[i].Type;
                        }
                        ApplicationData.Current.LocalSettings.Values["Random"] = item;
                        return true;
                    }
                }
            }

            ApplicationData.Current.LocalSettings.Values["Random"] =
            (
                from i
                in this.Randoms
                select (byte)(int)i.Type
            ).ToArray();
            return false;
        }

        public bool ReadCollection()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Collection"))
            {
                if (ApplicationData.Current.LocalSettings.Values["Collection"] is byte[] item)
                {
                    int h = this.Collection.Height();
                    int w = this.Collection.Width();

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            int i = y * w + x;
                            if (i < item.Length)
                            {
                                this.Collection[0, y, x] = (ChessType)(int)item[i];
                            }
                            else
                            {
                                this.Collection[0, y, x] = default;
                            }
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public bool WriteCollection()
        {
            int h = this.Collection.Height();
            int w = this.Collection.Width();
            int length = h * w;

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Collection"))
            {
                if (ApplicationData.Current.LocalSettings.Values["Collection"] is byte[] item)
                {
                    if (item.Length == length)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            for (int x = 0; x < w; x++)
                            {
                                int i = y * w + x;
                                item[i] = (byte)(int)this.Collection[0, y, x];
                            }
                        }
                        ApplicationData.Current.LocalSettings.Values["Collection"] = item;
                        return true;
                    }
                }
            }

            byte[] item2 = new byte[length];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int i = y * w + x;
                    item2[i] = (byte)(int)this.Collection[0, y, x];
                }
            }
            ApplicationData.Current.LocalSettings.Values["Collection"] = item2;
            return false;
        }
    }
}