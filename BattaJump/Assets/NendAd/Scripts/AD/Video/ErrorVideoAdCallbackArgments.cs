namespace NendUnityPlugin.AD.Video
{
	internal class ErrorVideoAdCallbackArgments : VideoAdCallbackArgments
	{
		public int errorCode;

		internal ErrorVideoAdCallbackArgments(NendAdVideo.VideoAdCallbackType type, int code) : base(type)
		{
			errorCode = code;
		}
	}
}


