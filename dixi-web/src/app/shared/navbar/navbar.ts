import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Sun, Moon, LogIn } from 'lucide-angular';
import { ThemeService } from '../../core/theme.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, LucideAngularModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class NavbarComponent {
  showLogin = input(true);
  readonly SunIcon = Sun;
  readonly MoonIcon = Moon;
  readonly LogInIcon = LogIn;

  constructor(public theme: ThemeService) {}
}