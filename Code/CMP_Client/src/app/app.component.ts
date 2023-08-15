import { Component } from '@angular/core';
import * as Notiflix from 'notiflix';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClinicMgmt';

  constructor(){
    Notiflix.Notify.init({
      width: '280px',
      position: 'center-top',
      distance: '20px',
      opacity:1,
      borderRadius: '5px',
      rtl:false,
      plainText: true,
      className:'notiflix-notify',
      clickToClose:false,
      fontFamily:'QuickSand',
      fontSize:'18px',
      timeout:3000,
      pauseOnHover:true
    })

    Notiflix.Loading.init({
      backgroundColor: 'transparent',
      svgColor:'blue',
      messageColor:'#344A53',
      messageFontSize:'30px'
    })

    Notiflix.Confirm.init({
      backOverlayColor:'transparent',
      okButtonBackground:'coral',
      cancelButtonBackground:'green',
      messageColor:'coral',
      titleColor:'blue',

    })
  }

}
