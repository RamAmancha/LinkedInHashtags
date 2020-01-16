import { Component, Inject, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getBaseUrl } from '../../main';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})

export class HomeComponent {

    private headers: HttpHeaders;
    //constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    //    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    //}

    public hashtags: ViewKeywords[];

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<ViewKeywords[]>(baseUrl + 'api/HashTag/GetAllTopHashTags').subscribe(result => {
            this.hashtags = result;
        }, error => console.error(error));
    }

    public mylinkedpost = 'Your Post';
    public myHeading = 'Your Heading';
    public myDesc = 'Description';
    public myBrandTags = '#tag';
    public myPersonalTags = '';
    public myPostType = '';
    public myPostFeed = '';
    public posttypeselected = '';
    public btncopystatus = false;
    @Input() options: Array<Object> = [
        { value: 1, name: "News" },
        { value: 2, name: "Insights" },
        { value: 3, name: "Update" },
        { value: 4, name: "Promotion" },
        { value: 5, name: "Analysis" },
        { value: 6, name: "Report" },
        { value: 7, name: "Research" },
        { value: 8, name: "Life Style" },
        { value: 9, name: "Quick Tip" }

    ];


    fnHeading(event: any) {
        this.myHeading = event.target.value;
    }

    fnGenKeywords() {
        this.fnDBCall(this.http, this.myDesc);
        this.myPersonalTags = 'Loading Please wait...';
        this.btncopystatus = true;
    }

    fnDBCall(http: HttpClient, param1: string) {
        const headers = new HttpHeaders().set('Content-Type', 'text/plain; charset=utf-8');

        http.get<string>('http://linkedinpost-prod.ap-south-1.elasticbeanstalk.com/api/HashTag/GetHashTag' + '?inputtext=' + param1, { headers, responseType: 'text' as 'json' }).subscribe(result => {
            this.myPersonalTags = result.toString();
        }, error => console.error(error));
    }

    funDesc(event: any) {
        this.myDesc = event.target.value;
    }

    fnTags(event: any) {
        this.myBrandTags = event.target.value;
    }

    fnCopyToClipboard(event: any) {
        var copytext = document.getElementById("LIPostFeed").innerText;

        var copyElement = document.createElement("textarea");
        copyElement.style.position = 'fixed';
        copyElement.style.opacity = '0';
        copyElement.textContent = copytext;
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(copyElement);
        copyElement.select();
        document.execCommand('copy');
        body.removeChild(copyElement);

    }

    lineBreak() {
        return filterFilter;
        function filterFilter(text) {
            if (!text || !text.length) {
                return text;
            }

            return text.replace(/(\\r\\n)|([\r\n])/gmi, '<br/>');
        }
    }

}

interface ViewKeywords {
    keyword: string;
    count: number;
}