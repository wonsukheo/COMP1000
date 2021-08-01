using System.Drawing;
using System;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            uint length = (uint)(sigma * 6);
            
            if (length % 2 == 0)
            {
                length++;
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
            int median = (filter.Length + 1) / 2;
            int distribution = (filter.Length - 1) / 2;

            double[] filteredSignal = new double[signal.Length];

            for (int i = 0; i < signal.Length; i++)
            {
                double value = signal[i] * filter[median - 1];

                for (int j = 1; j <= distribution; j++)
                {
                    if (i - j >= 0)
                    {
                        value += signal[i - j] * filter[median - 1 - j];
                    }
                    if (i + j < signal.Length)
                    {
                        value += signal[i + j] * filter[median - 1 + j];
                    }    
                }
                filteredSignal[i] = value;
                
            }
            return filteredSignal;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            uint length = (uint)(sigma * 6);

            if (length % 2 == 0)
            {
                length++;
            }

            int distribution = (int)(length / 2);
            double[,] gaussianFilter2D = new double[length, length];
            double denominator = 2 * sigma * sigma;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    double denom = denominator * Math.PI;
                    double numer = Math.Exp( ( ( (i - distribution) * (i - distribution) + (j - distribution) * (j - distribution) ) / -denominator) );
                    gaussianFilter2D[i, j] = numer / denom;
                }
            }

            return gaussianFilter2D;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            int r = filter.GetLength(0);
            int c = filter.GetLength(1);
            double[,] convolveFilter = new double[r, c];
            int median = r / 2;
            int distribution = r / 2;

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    convolveFilter[i, j] = filter[r - i - 1, c - j - 1];
                }
            }

            int bitmapR = bitmap.Height;
            int bitmapC = bitmap.Width;
            Bitmap convolvedImage = new Bitmap(bitmapC, bitmapR);

            for (int i = 0; i < bitmapR; i++)
            {
                for (int j = 0; j < bitmapC; j++)
                {
                    
                    Color pixelColor = bitmap.GetPixel(j, i);
                    
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
                                pixelColor = bitmap.GetPixel(j - l, i - k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median - k, median - l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median - k, median - l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median - k, median - l]);
                            }
                            if (k != 0 && l != 0 && i - k >= 0 && j + l < bitmapC)
                            {
                                pixelColor = bitmap.GetPixel(j + l, i - k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median - k, median + l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median - k, median + l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median - k, median + l]);
                            }
                            if (i + k < bitmapR && j + l < bitmapC)
                            {
                                pixelColor = bitmap.GetPixel(j + l, i + k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median + k, median + l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median + k, median + l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median + k, median + l]);
                            }
                            if (k != 0 && l != 0 && i + k < bitmapR && j - l >= 0)
                            {
                                pixelColor = bitmap.GetPixel(j - l, i + k);
                                newColorR += (double)(pixelColor.R * convolveFilter[median + k, median - l]);
                                newColorG += (double)(pixelColor.G * convolveFilter[median + k, median - l]);
                                newColorB += (double)(pixelColor.B * convolveFilter[median + k, median - l]);
                            }
                        }
                    }

                    Color newColor = Color.FromArgb((byte)newColorR, (byte)newColorG, (byte)newColorB);
                    convolvedImage.SetPixel(j, i, newColor);
                }
            }
            return convolvedImage;
        }
    }
}