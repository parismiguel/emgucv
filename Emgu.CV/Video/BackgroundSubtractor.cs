﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Emgu.CV
{
    public interface IBackgroundSubtractor : IAlgorithm
    {
        IntPtr BackgroundSubtractorPtr { get; }
    }

    /// <summary>
    /// A static class that provide extension methods to backgroundSubtractor
    /// </summary>
    public static class BackgroundSubtractorExtension
    {
        /// <summary>
        /// Update the background model
        /// </summary>
        /// <param name="image">The image that is used to update the background model</param>
        /// <param name="learningRate">Use -1 for default</param>
        /// <param name="fgMask">The output foreground mask</param>
        public static void Apply(this IBackgroundSubtractor substractor, IInputArray image, IOutputArray fgMask, double learningRate = -1)
        {
            using (InputArray iaImage = image.GetInputArray())
            using (OutputArray oaFgMask = fgMask.GetOutputArray())
                CvInvoke.cveBackgroundSubtractorUpdate(substractor.BackgroundSubtractorPtr, iaImage, oaFgMask, learningRate);
        }

        /// <summary>
        /// Computes a background image.
        /// </summary>
        /// <param name="backgroundImage">The output background image</param>
        /// <remarks> Sometimes the background image can be very blurry, as it contain the average background statistics.</remarks>
        public static void GetBackgroundImage(this IBackgroundSubtractor substractor, IOutputArray backgroundImage)
        {
            using (OutputArray oaBackgroundImage = backgroundImage.GetOutputArray())
                CvInvoke.cveBackgroundSubtractorGetBackgroundImage(substractor.BackgroundSubtractorPtr, oaBackgroundImage);
        }
    }
}

namespace Emgu.CV
{
    public static partial class CvInvoke
    {
        [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern void cveBackgroundSubtractorUpdate(IntPtr bgSubtractor, IntPtr image, IntPtr fgmask, double learningRate);

        [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern void cveBackgroundSubtractorGetBackgroundImage(IntPtr bgSubtractor, IntPtr backgroundImage);
    }
}