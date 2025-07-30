import 'zone.js';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './core/layout/header/header';
import { Home } from './core/pages/home/home';
import { Footer } from "./core/layout/footer/footer";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Home, Footer],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
