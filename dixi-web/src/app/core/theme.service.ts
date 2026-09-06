import { Injectable, signal, effect } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  theme = signal<'light' | 'dark'>(
    (localStorage.getItem('dixi-theme') as 'light' | 'dark') ?? 'dark'
  );

  constructor() {
    effect(() => {
      document.documentElement.setAttribute('data-theme', this.theme());
      localStorage.setItem('dixi-theme', this.theme());
    });
  }

  toggle() {
    this.theme.set(this.theme() === 'light' ? 'dark' : 'light');
  }
}