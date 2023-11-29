import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {Subscription} from "rxjs";

interface WeathersHistoryResponse {
  message: string;
  weatherHistory: WeatherDTO[];
}

interface WeatherResponse {
  message: string;
  weatherDTO: WeatherDTO;
}

interface WeatherDTO {
  city: CityDTO;
  date: Date;
  temperature: TemperatureDTO;
  wind: WindDTO;
}

interface CityDTO {
  id: number;
  name: string;
}

interface TemperatureDTO {
  temperature: number;
}

interface WindDTO{
  power: number;
  windDirection: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  private request!: Subscription;
  public weather!: WeatherDTO | undefined;
  public weatherHistory!: WeatherDTO[] | undefined;
  public cities!: CityDTO[];
  public cityId!: number;
  public isDownloading: boolean = false;
  public errorText: string = '';

  constructor(private http: HttpClient) {
  }

  getWeather(isCanceled: boolean) {
    this.isDownloading = true;
    if(isCanceled){
      this.request.unsubscribe();
      this.isDownloading = false;
      this.weather = undefined;
      return;
    }
    this.request = this.http.get<WeatherResponse>('https://localhost:7227/GetByCityId/' + this.cityId).subscribe(
      (result) => {
        this.weather = result.weatherDTO;
        this.isDownloading = false;
      },
      (error) => {
        console.error(error);
        this.errorText = error.message;
        this.isDownloading = false;
      }
    );
  }

  getWeatherHistory(isCanceled: boolean) {
    this.isDownloading = true;
    if(isCanceled){
      this.request.unsubscribe();
      this.isDownloading = false;
      this.weatherHistory = undefined;
      return;
    }
    this.request = this.http.get<WeathersHistoryResponse>('https://localhost:7227/GetWeatherHistoryByCityId/' + this.cityId).subscribe(
      (result) => {
        this.weatherHistory = result.weatherHistory;
        this.isDownloading = false;
      },
      (error) => {
        console.error(error);
        this.errorText = error.message;
        this.isDownloading = false;
      }
    );
  }

  ngOnInit(): void {
    this.http.get<CityDTO[]>('https://localhost:7227/GetCities').subscribe(
      (result) => {
        this.cities = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'WeatherSimulator.Web';
}
