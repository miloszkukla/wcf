// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;

namespace System.ServiceModel.Channels
{
    internal static class AuthenticationSchemesHelper
    {
        public static bool IsSingleton(this AuthenticationSchemes v)
        {
            bool result;
            switch (v)
            {
                case AuthenticationSchemes.Digest:
                case AuthenticationSchemes.Negotiate:
                case AuthenticationSchemes.Ntlm:
                case AuthenticationSchemes.Basic:
                case AuthenticationSchemes.Anonymous:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        public static bool IsSet(this AuthenticationSchemes thisPtr, AuthenticationSchemes authenticationSchemes)
        {
            return (thisPtr & authenticationSchemes) == authenticationSchemes;
        }

        public static bool IsNotSet(this AuthenticationSchemes thisPtr, AuthenticationSchemes authenticationSchemes)
        {
            return (thisPtr & authenticationSchemes) == 0;
        }

        internal static string ToString(AuthenticationSchemes authScheme)
        {
            return authScheme.ToString().ToLowerInvariant();
        }
    }
}
