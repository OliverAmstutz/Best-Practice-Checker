using System;
using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    [Serializable]
    public sealed class TextureRatioResultContent : IResult
    {
        [SerializeField] private Status status = BusinessLogic.Status.NotCalculated;
        [SerializeField] private List<FaultyTexture> faultyTextures = new List<FaultyTexture>();

        public string Content()
        {
            return status switch
            {
                BusinessLogic.Status.Ok =>
                    "All your texture's width and height are based on two. Texture compression is possible.\n" +
                    " Visit this best practice documentation for further details sources.",
                BusinessLogic.Status.Warning =>
                    "You have textures which are not based on two - no texture compression is possible for these textures!\n" +
                    " It is highly recommended to update the listed textures below in order to decrease required memory",
                _ => "Something went wrong in the texture ratio initialization!"
            };
        }

        public void Status(Status packageStatus)
        {
            status = packageStatus;
        }

        public void AddFaultyTexture(FaultyTexture texture)
        {
            faultyTextures.Add(texture);
        }

        public List<FaultyTexture> FaultyTextures()
        {
            return faultyTextures;
        }
    }
}