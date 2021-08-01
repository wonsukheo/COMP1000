using System.Drawing;
using System;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            double len = sigma * 6;
            uint length = (uint)len;

            if (length % 2 == 0)
            {
                length++;
            }
            if (len > length)
            {
                length += 2;
            }

            int distribution = (int)(length / 2);
            double[] gaussianFilter1D = new double[length];
            double denominator = sigma * (Math.Sqrt(Math.PI * 2));

            for (int i = 0; i < length; i++)
            {
                double exponent = -(i - distribution) * (i - distribution) / (2.0 * sigma * sigma);
                gaussianFilter1D[i] = Math.Exp(exponent) / denominator;
            }
             
            return gaussianFilter1D;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            int median = (filter.Length - 1) / 2;
            int distribution = (filter.Length - 1) / 2;

            double[] reversedFilter = new double[filter.Length];
            
            for (int i = 0; i < filter.Length; i++)
            {
                reversedFilter[i] = filter[filter.Length - 1 - i];
            }

            double[] filteredSignal = new double[signal.Length];

            for (int i = 0; i < signal.Length; i++)
            {
                double value = signal[i] * reversedFilter[median];

                for (int j = 1; j <= distribution; j++)
                {
                    if (i - j >= 0)
                    {
                        value += signal[i - j] * reversedFilter[median - j];
                    }
                    if (i + j < signal.Length)
                    {
                        value += signal[i + j] * reversedFilter[median + j];
                    }    
                }
                
                filteredSignal[i] = value;
            }
            return filteredSignal;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            double len = sigma * 6;
            uint length = (uint)len;

            if (length % 2 == 0)
            {
                length++;
            }
            if (len > length)
            {
                length += 2;
            }

            int distribution = (int)(length / 2);
            double[,] gaussianFilter2D = new double[length, length];
            double denominator = 2 * sigma * sigma;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    double denom = denominator * Math.PI;
                    double numer = Math.Exp((((i - distribution) * (i - distribution) + (j - distribution) * (j - distribution)) / -denominator));
                    gaussianFilter2D[i, j] = numer / denom;
                }
            }

            return gaussianFilter2D;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            int len = filter.GetLength(0);
            double[,] convolveFilter = new double[len, len];
            int median = len / 2;
            int distribution = len / 2;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    convolveFilter[i, j] = filter[len - i - 1, len - j - 1];
                }
            }

            int bitmapR = bitmap.Height;
            int bitmapC = bitmap.Width;
            Bitmap originalImage = new Bitmap(bitmap);
            Bitmap convolvedImage = new Bitmap(bitmapC, bitmapR);

            for (int i = 0; i < bitmapR; i++)
            {
                for (int j = 0; j < bitmapC; j++)
                {
                    Color pixelColor = originalImage.GetPixel(j, i);
                    
                    double newColorR = (double)(pixelColor.R * convolveFilter[median, median]);
                    double newColorG = (double)(pixelColor.G * convolveFilter[median, median]);
                    double newColorB = (double)(pixelColor.B * convolveFilter[median, median]);
                    
                    for (int k = 0; k <= distribution; k++)
                    {
                        for (int l = 0; l <= distribution; l++)
                        {
                            if (k == 0 && l == 0)
                            {
                                continue;
                            }

                            if (i - k >= 0 && j - l >= 0)
                            {
                                pixelColor = originalImage.GetPixel(j - l, i - k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median - k, median - l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median - k, median - l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median - k, median - l]);
                            }
                            if (k != 0 && l != 0 && i - k >= 0 && j + l < bitmapC)
                            {
                                pixelColor = originalImage.GetPixel(j + l, i - k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median - k, median + l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median - k, median + l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median - k, median + l]);
                            }
                            if (i + k < bitmapR && j + l < bitmapC)
                            {
                                pixelColor = originalImage.GetPixel(j + l, i + k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median + k, median + l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median + k, median + l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median + k, median + l]);
                            }
                            if (k != 0 && l != 0 && i + k < bitmapR && j - l >= 0)
                            {
                                pixelColor = originalImage.GetPixel(j - l, i + k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median + k, median - l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median + k, median - l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median + k, median - l]);
                            }
                        }
                    }
                    int newR = (int)newColorR % 256;
                    int newG = (int)newColorG % 256;
                    int newB = (int)newColorB % 256;
                    Color newColor = Color.FromArgb((byte)newR, (byte)newG, (byte)newB);
                    convolvedImage.SetPixel(j, i, newColor);
                }
            }
            return convolvedImage;
        }
    }
}