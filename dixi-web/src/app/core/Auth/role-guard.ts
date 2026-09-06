import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth';

export function roleGuard(allowedRole: string): CanActivateFn {
    return () => {
        const auth = inject(AuthService);
        const router = inject(Router);
        const user = auth.currentUser();

        if (!user) {
            router.navigate(['/login']);
            return false;
        }
        if (user.role !== allowedRole) {
            router.navigate(['/login']);
            return false;
        }
        return true;
    };
}