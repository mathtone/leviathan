// Extending the Window interface
declare global {
   interface Window {
      CookieManager: CookieManagerClass;
      navigateTo: (url: string) => void;
   }
}

class CookieManagerClass {
   setCookie(name: string, value: string, days?: number): void {
      let expires = "";
      if (days) {
         const date = new Date();
         date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
         expires = "; expires=" + date.toUTCString();
      }
      document.cookie = name + "=" + (value || "") + expires + "; path=/";
   }

   getCookie(name: string): string | null {
      const nameEQ = name + "=";
      const ca = document.cookie.split(';');
      for (let i = 0; i < ca.length; i++) {
         let c = ca[i];
         while (c.charAt(0) === ' ') c = c.substring(1, c.length);
         if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
      }
      return null;
   }

   eraseCookie(name: string): void {
      document.cookie = name + '=; Max-Age=-99999999;';
   }
}

// Assign the new CookieManagerClass instance to window
window.CookieManager = new CookieManagerClass();

// Exported function
export function navigateTo(url: string): void {
   window.location.href = url;
}

// Bind the navigateTo function to window
window.navigateTo = navigateTo;

// The rest of your TypeScript code (if any) goes here.
