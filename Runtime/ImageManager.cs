// Discord Game SDK v3.2.1
// https://discord.com/developers/docs/developer-tools/game-sdk

#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX) && !UNITY_SERVER
#define UNITY_PC
#endif

#if UNITY_PC
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Discord
{
	public partial struct ImageHandle
    {
        static public ImageHandle User(Int64 id)
        {
            return User(id, 128);
        }

        static public ImageHandle User(Int64 id, UInt32 size)
        {
            return new ImageHandle
            {
                Type = ImageType.User,
                Id = id,
                Size = size,
            };
        }
    }

    public partial class ImageManager
    {
        public void Fetch(ImageHandle handle, FetchHandler callback)
        {
            Fetch(handle, false, callback);
        }

        public byte[] GetData(ImageHandle handle)
        {
            var dimensions = GetDimensions(handle);
            var data = new byte[dimensions.Width * dimensions.Height * 4];
            GetData(handle, data);
            return data;
        }

        public Texture2D GetTexture(ImageHandle handle)
        {
            var dimensions = GetDimensions(handle);
            var texture = new Texture2D((int)dimensions.Width, (int)dimensions.Height, TextureFormat.RGBA32, false, true);
            texture.LoadRawTextureData(GetData(handle));
            texture.Apply();
            return texture;
        }
    }
}
#endif