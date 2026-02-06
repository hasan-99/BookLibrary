using System;

namespace BookLibrary.Helpers {
    public static class ErrorHandler {
        public static string GetUserMessage(Exception ex) {
            return "An unexpected error occurred. Please try again.";
        }
    }
}
