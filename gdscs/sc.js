var i=0;
var ii=0;
var s0='';	

function pre()
{
	FP_preloadImgs(
		'images/buttonC.jpg'
		, 'images/buttonD.jpg'
		, 'images/buttonF.jpg'
		, 'images/button10.jpg'

		, 'images/y0.gif'
		, 'images/y0l.gif'
		, 'images/y0r.gif'
		, 'images/y1l.gif'
		, 'images/y1r.gif'
		, 'images/y2l.gif'
		, 'images/y2r.gif'
		, 'images/y3l.gif'
		, 'images/y3r.gif'
		, 'images/y4l.gif'
		, 'images/y4r.gif'
		, 'images/y5r.gif'
		, 'images/y5l.gif'
		, 'images/yyl.gif'
		, 'images/yyr.gif'
		, 'images/yyy.gif'
		
		, 'images/z0.gif'
		, 'images/z0l.gif'
		, 'images/z0r.gif'
		, 'images/z1l.gif'
		, 'images/z1r.gif'
		, 'images/z2l.gif'
		, 'images/z2r.gif'
		, 'images/z3l.gif'
		, 'images/z3r.gif'
		, 'images/z4l.gif'
		, 'images/z4r.gif'
		, 'images/z5r.gif'
		, 'images/z5l.gif'
		, 'images/zzl.gif'
		, 'images/zzr.gif'
		, 'images/zzz.gif'
		)	
}

function FP_preloadImgs() {//v1.0
 var d=document,a=arguments; if(!d.FP_imgs) d.FP_imgs=new Array();
 for(var i=0; i<a.length; i++) { d.FP_imgs[i]=new Image; d.FP_imgs[i].src=a[i]; }
}

function FP_swapImg() {//v1.0
 var doc=document,args=arguments,elm,n; doc.$imgSwaps=new Array(); for(n=2; n<args.length;
 n+=2) { elm=FP_getObjectByID(args[n]); if(elm) { doc.$imgSwaps[doc.$imgSwaps.length]=elm;
 elm.$src=elm.src; elm.src=args[n+1]; } }
}

function FP_getObjectByID(id,o) {//v1.0
 var c,el,els,f,m,n; if(!o)o=document; if(o.getElementById) el=o.getElementById(id);
 else if(o.layers) c=o.layers; else if(o.all) el=o.all[id]; if(el) return el;
 if(o.id==id || o.name==id) return o; if(o.childNodes) c=o.childNodes; if(c)
 for(n=0; n<c.length; n++) { el=FP_getObjectByID(id,c[n]); if(el) return el; }
 f=o.forms; if(f) for(n=0; n<f.length; n++) { els=f[n].elements;
 for(m=0; m<els.length; m++){ el=FP_getObjectByID(id,els[n]); if(el) return el; } }
 return null;
}

function cf(f1,f2)
{
    var f1=document.forms[f1];
    var f2=document.forms[f2];
    var s='';
    
    for (var i=0;i<f1.v.length;i++)
    {
        if (f1.v[i].checked)
            {s+=f1.v[i].value+',';}
    }
    f2.v.value=s;
    f2.submit();
}
function dist2(frm,d,r,t)
{
    CompDist.innerText=d;
    CompRegn.innerText=r;
    frm.t.disabled=t;
    frm.t.checked=!t;
    frm.n.disabled=t;
    frm.n.checked=!t;
    frm.cmdOK.disabled=0;
}
function dist3(t)
{
	var el = t;
	var iCnt = 0;var sArr;
	for (var i=0;i<el.v.length;i++)
	{
		if (el.v[i].checked)
		{
			sArr=el.v[i].value;
		}
	}
   	var oArr=sArr.split('~');
	if (sArr==null)
	{
        alert('Please select an option \nThank You.');
	}
	else
	{
        el.d.value=oArr[0];
        el.r.value=oArr[1];
    	el.submit();
	}
}
function dsel(t)
{
	var el = t
	var iCnt = 0;var s;
	for (var i=0;i<el.v.length;i++)
	{
		if (el.v[i].checked)
		{
			s=el.v[i].value;
			iCnt ++;
		}
	}
	if (iCnt!=1)
	{
	    alert('Please select a variable. \nThank you.');
	}
	else
	{
	document.forms['f2'].v.value=s;
	document.forms['f2'].submit();
	}
}
function mz(w,c)
{
	var el = document.forms[w];
	var iCnt = 0;
	for (var i=0;i<el.v.length;i++)
	{
		if (el.v[i].checked)
		{
			iCnt ++;
		}
	}
	if(c==1)
	{
	    if(iCnt!=2)
	    {
   		    alert('Anda harus memilih dua variabel. \n\nYou must select exactly two variables.');
		    return false;
	    }
	}
	else
	{
	    if(iCnt!=1)
	    {
   		    alert('Anda belum memilih variabel. \n\nYou have not selected any variable. ');
		    return false;
	    }
	}
}
function shb(id)
{	
	if ( (s0=='')||(s0==id) )
	{
		if (document.all[id].style.display=='none')
		{
			document.all[id].style.display='block';
			document.all[id + 'img'].src='images/minus.gif';
		}
		else
		{
			document.all[id].style.display='none';
			document.all[id + 'img'].src='images/plus.gif';
		}
		s0=id;
	}
	else
	{
		if (document.all[id].style.display=='none')
		{
			document.all[id].style.display='block';
			document.all[id + 'img'].src='images/minus.gif';

			document.all[s0].style.display='none';
			document.all[s0 + 'img'].src='images/plus.gif';
		}
		else
		{
			document.all[id].style.display='none';
			document.all[id + 'img'].src='images/plus.gif';

			document.all[s0].style.display='block';
			document.all[s0 + 'img'].src='images/minus.gif';
		}
		s0=id;
	}
}

function sh(id)
{
	if (document.all[id].style.display=='none')
	{
		document.all[id].style.display='block';
		document.all[id + 'img'].src='images/fmin.gif';
	}
	else
	{
		document.all[id].style.display='none';
		document.all[id + 'img'].src='images/fplus.gif';
	}
}
function sh0(id)
{
	if (document.all[id].style.display=='none')
	{
		document.all[id].style.display='block';
		document.all[id + 'img'].src='images/minus.gif';
	}
	else
	{
		document.all[id].style.display='none';
		document.all[id + 'img'].src='images/plus.gif';
	}
}
function s(id,v)
{
	if (v==0)
	{
		document.all[id].style.visibility='hidden';
	}
	else
	{
		document.all[id].style.visibility='visible';
	}
}

function ct(id)
{
	mii(id);
	if (document.all[id].checked && ii<=5)
	{
		document.all['txt' + id ].style.visibility='visible';
		//ii++;
	}
	else
	{
		document.all['txt' + id ].style.visibility='hidden';
		//ii--;
	}
	if (ii>5)
	{
		document.all[id].checked = false;
		document.all['txt' + id ].style.visibility='hidden';
		//alert('Please select only five variables. \n The OK button will still be disabled \n Thank You')
		document.all['cmdOK'].disabled=1;
	}
}

function c(n)
{
    	window.event.srcElement.className=n;
}
function cz(n)
{   
    var cZ = window.event.srcElement;
    if (cZ.tagName.toLowerCase=='div')
    	cZ.className=n;
}
function cc(o,n)
{
	document.all[o].className=n;
}
function ca()
{
	switch (window.event.srcElement.className)
	{
		case ('i0'):
			window.event.srcElement.className='i0O';break;
		case ('i0O'):
			window.event.srcElement.className='i0';break;
		case ('i1'):
			window.event.srcElement.className='i1O';break;
		case ('i1O'):
			window.event.srcElement.className='i1';break;
		case ('i2'):
			window.event.srcElement.className='i2O';break;
		case ('i2O'):
			window.event.srcElement.className='i2';break;
		case ('i3'):
			window.event.srcElement.className='i3O';break;
		case ('i3O'):
			window.event.srcElement.className='i3';break;
/*
		case ('i4'):
			window.event.srcElement.className='i4O';break;
		case ('i4O'):
			window.event.srcElement.className='i4';break;
*/
		case ('i4'):
			window.event.cancelBubble=true;break;
		case ('i4O'):
			window.event.cancelBubble=true;break;
	}
}
function o(u)
{	
	window.navigate(u);
}
function show(id)
{
	document.all[id].style.display='';
	document.all[id + 'img'].src='images/minus.gif';
}
function hide(id)
{
	document.all[id].style.display='none';
	document.all[id + 'img'].src='images/plus.gif';
}

function ci(id,t)
{
	if (document.all[id].style.visibility=='hidden')
	{
		document.all[id].style.visibility='visible';
	}
	else
	{
		document.all[id].style.visibility='hidden';
	}
	document.all[id].innerHTML=t;
}
function ciD(id,d,r)
{
	if (document.all[id].style.visibility=='hidden')
	{
		document.all[id].style.visibility='visible';
	}
	else
	{
		document.all[id].style.visibility='hidden';
	}
	document.all[id].innerHTML='District : ' + d + '<BR>' + ' Province : ' + r;
}
function cm(id)
{
	document.all[id].style.pixelLeft = document.body.scrollLeft+event.clientX;
	document.all[id].style.pixelTop = document.body.scrollTop+event.clientY;
}
function ch(id)
{
	document.all[id].style.visibility='hidden';
}
function chs(w,n)
{
	w.className=n;
}

function shV(st)
{
	for(var i=0;i<document.all.length;i++)
	{
		if((document.all(i).id.indexOf("d")==0)&&(document.all(i).tagName=="DIV"))
		{
			document.all(i).style.display=st;
		}
	}
}

function fnSave()
{
	var sArr="";
	var iCnt = 0;
	for(var i=0;i<document.all.length;i++)
	{
		oTrap=document.all[i];
		if ((oTrap.id.indexOf("d")==0)&&(oTrap.tagName=="DIV"))
		{
			if(sArr.length==0)
			{
				sArr=oTrap.style.display;
			}
			else sArr+="|" + oTrap.style.display;
		}
	}
	document.all['TOC'].setAttribute("z",sArr);
}

function fnLoad()
{
	var sArr=document.all['TOC'].getAttribute("z");
	var cyc=0;
	var oArr=sArr.split("|");
	for (var i=0;i<document.all.length;i++)
	{
		oTrap=document.all[i];
		if ((oTrap.id.indexOf("d")==0)&&(oTrap.tagName=="DIV"))
		{
   			oTrap.style.display=oArr[cyc];
			cyc++;
		}
	}
}


		var err=0;
		function vl(n)
		{
			var m=eval('document.frmVar.' + n + '.options[document.frmVar.' + n + '.selectedIndex].value');
			if(m==0)
			{
				alert('Maaf, pilihan Anda tidak sah.\nPilihlah dari teks yang berlatar belakang putih \n\nSorry, the selection is not valid.\nChoose from the text with the white background');
				if (document.getElementById)
				{
					document.all('err' + n).innerHTML="&lt;-Error ";
					document.all('err' + n).title= 'The value must be the deepest level of hierarchy';
				}
				err++;
			}
			else
			{
				if (document.getElementById)
				{
					document.all('err' + n).innerHTML="";
					document.all('err' + n).title='';
				}
				err--;
			}
		}
		function vt(n)
		{
			var z=eval('document.frmVar.' + n + '.value');
			if ((isNaN(z))||(z>10))
			{
				alert('Masukan harus angka dan maksimum 10. \n\nMust be a number and less than or equals 10.');
				if (document.getElementById)
				{
					document.all('err' + n).innerHTML="&lt;-Error ";
					document.all('err' + n).title='The value must numeric and less than 1 (one)';
				}
				err++;
			}
			else
			{
				if (document.getElementById)
				{
					document.all('err' + n).innerHTML="";
					document.all('err' + n).title='';
				}
				err--;
			}
			var vt0=parseFloat(document.frmVar.vt0.value); 
			var vt1=parseFloat(document.frmVar.vt1.value); 
			var vt2=parseFloat(document.frmVar.vt2.value); 
			var vt3=parseFloat(document.frmVar.vt3.value); 
			var vt4=parseFloat(document.frmVar.vt4.value); 
			document.frmVar.vt10.value= vt0+ vt1 + vt2 + vt3 + vt4;
		}

function fs2()
{
	if(document.f1.ds.options[document.f1.ds.selectedIndex].value==4)
	{alert('We are sorry.\nthe Bureaucrat data set is not available at this moment.\nPlease select other data sets.')	}
}



function fs(e)
{
	var ns4 = (document.layers)? true:false;
	var ie4 = (document.all)? true:false;
	var r1 = document.f1.r;
	var d1 = document.f1.d;
	var dd;
	if (ie4)
	{
		dd=document.all.boxD;
		dd.style.visibility='hidden';
	}
	for (var i = d1.options.length; i >= 0; i--) d1.options[i] = null;
	d1.options[d1.options.length] = new Option('- Select a district -','All Districts');
	if(e.selectedIndex!=-1)
	{
		if(e.options[e.selectedIndex].value=='JAWA BARAT')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Purwakarta','PURWAKARTA');
			d1.options[d1.options.length] = new Option('Kota Banjar','KOTA BANJAR');
			d1.options[d1.options.length] = new Option('Subang','SUBANG');
			d1.options[d1.options.length] = new Option('Cirebon','CIREBON');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='JAWA TENGAH')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Pati','PATI');
			d1.options[d1.options.length] = new Option('Blora','BLORA');
			d1.options[d1.options.length] = new Option('Demak','DEMAK');
			d1.options[d1.options.length] = new Option('Kota Surakarta','KOTA SURAKARTA');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='JAWA TIMUR')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Bangkalan','BANGKALAN');
			d1.options[d1.options.length] = new Option('Nganjuk','NGANJUK');
			d1.options[d1.options.length] = new Option('Kota Blitar','KOTA BLITAR');
			d1.options[d1.options.length] = new Option('Bojonegoro','BOJONEGORO');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='KALIMANTAN SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Banjarmasin','KOTA BANJARMASIN');
			d1.options[d1.options.length] = new Option('Barito Kuala','BARITO KUALA');
			d1.options[d1.options.length] = new Option('Hulu Sungai Utara','HULU SUNGAI UTARA');
			d1.options[d1.options.length] = new Option('Hulu Sungai Tengah','HULU SUNGAI TENGAH');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='NUSA TENGGARA BARAT')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Bima','BIMA');
			d1.options[d1.options.length] = new Option('Lombok Tengah','LOMBOK TENGAH');
			d1.options[d1.options.length] = new Option('Lombok Timur','LOMBOK TIMUR');
			d1.options[d1.options.length] = new Option('Kota Bima','KOTA BIMA');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='SULAWESI SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Polewali Mamasa','POLEWALI MAMASA');
			d1.options[d1.options.length] = new Option('Bantaeng','BANTAENG');
			d1.options[d1.options.length] = new Option('Bulukumba','BULUKUMBA');
			d1.options[d1.options.length] = new Option('Kota Palopo','KOTA PALOPO')
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='SUMATERA UTARA')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Binjai','KOTA BINJAI');
			d1.options[d1.options.length] = new Option('Tapanuli Selatan','TAPANULI SELATAN');
			d1.options[d1.options.length] = new Option('Asahan','ASAHAN');
			d1.options[d1.options.length] = new Option('Labuhan Batu','LABUHAN BATU');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
		if(e.options[e.selectedIndex].value=='SUMATERA SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Palembang','KOTA PALEMBANG');
			d1.options[d1.options.length] = new Option('Musi Rawas','MUSI RAWAS');
			d1.options[d1.options.length] = new Option('Banyuasin','BANYUASIN');
			d1.options[d1.options.length] = new Option('Ogan Komering Ulu','OGAN KOMERING ULU');
			d1.options[d1.options.length] = new Option('All of the districts','All Districts');		}
	}
}

function fsz(e,r1,d1,lang)
{
	var ns4 = (document.layers)? true:false;
	var ie4 = (document.all)? true:false;
	var dd;
	if (ie4)
	{
		dd=document.getElementById('boxD');
		dd.style.visibility='hidden';
	}
	for (var i = d1.options.length; i >= 0; i--) d1.options[i] = null;
	if (lang==0)//id-ID
	{d1.options[d1.options.length] = new Option('- Pilih kabupaten -','All Districts');}
	else
	{d1.options[d1.options.length] = new Option('- Select a district -','All Districts');}
	if(e.selectedIndex!=-1)
	{
		if(e.options[e.selectedIndex].value=='JAWA BARAT')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Purwakarta','PURWAKARTA');
			d1.options[d1.options.length] = new Option('Kota Banjar','KOTA BANJAR');
			d1.options[d1.options.length] = new Option('Subang','SUBANG');
			d1.options[d1.options.length] = new Option('Cirebon','CIREBON');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='JAWA TENGAH')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Pati','PATI');
			d1.options[d1.options.length] = new Option('Blora','BLORA');
			d1.options[d1.options.length] = new Option('Demak','DEMAK');
			d1.options[d1.options.length] = new Option('Kota Surakarta','KOTA SURAKARTA');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='JAWA TIMUR')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Bangkalan','BANGKALAN');
			d1.options[d1.options.length] = new Option('Nganjuk','NGANJUK');
			d1.options[d1.options.length] = new Option('Kota Blitar','KOTA BLITAR');
			d1.options[d1.options.length] = new Option('Bojonegoro','BOJONEGORO');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='KALIMANTAN SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Banjarmasin','KOTA BANJARMASIN');
			d1.options[d1.options.length] = new Option('Barito Kuala','BARITO KUALA');
			d1.options[d1.options.length] = new Option('Hulu Sungai Utara','HULU SUNGAI UTARA');
			d1.options[d1.options.length] = new Option('Hulu Sungai Tengah','HULU SUNGAI TENGAH');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='NUSA TENGGARA BARAT')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Bima','BIMA');
			d1.options[d1.options.length] = new Option('Lombok Tengah','LOMBOK TENGAH');
			d1.options[d1.options.length] = new Option('Lombok Timur','LOMBOK TIMUR');
			d1.options[d1.options.length] = new Option('Kota Bima','KOTA BIMA');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='SULAWESI SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Polewali Mamasa','POLEWALI MAMASA');
			d1.options[d1.options.length] = new Option('Bantaeng','BANTAENG');
			d1.options[d1.options.length] = new Option('Bulukumba','BULUKUMBA');
			d1.options[d1.options.length] = new Option('Kota Palopo','KOTA PALOPO')
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='SUMATERA UTARA')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Binjai','KOTA BINJAI');
			d1.options[d1.options.length] = new Option('Tapanuli Selatan','TAPANULI SELATAN');
			d1.options[d1.options.length] = new Option('Asahan','ASAHAN');
			d1.options[d1.options.length] = new Option('Labuhan Batu','LABUHAN BATU');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
		if(e.options[e.selectedIndex].value=='SUMATERA SELATAN')		{
			if(ie4){dd.style.visibility='visible'}
			d1.options[d1.options.length] = new Option('Kota Palembang','KOTA PALEMBANG');
			d1.options[d1.options.length] = new Option('Musi Rawas','MUSI RAWAS');
			d1.options[d1.options.length] = new Option('Banyuasin','BANYUASIN');
			d1.options[d1.options.length] = new Option('Ogan Komering Ulu','OGAN KOMERING ULU');
			if (lang==0)//id-ID
			{d1.options[d1.options.length] = new Option('Seluruh Kabupaten','All Districts');}
			else
			{d1.options[d1.options.length] = new Option('All of the districts','All Districts');}					
			}
	}
}

function tt(w)
{
	var el = document.forms[w];
	var iCnt = 0;var s;
	for (var i=0;i<el.mode.length;i++)
	{
		if (el.mode[i].checked)
		{
			if(el.mode[i].value==1)
			{
				s='g1_dist.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value;
			}
			if(el.mode[i].value==2)
			{
				s='g1_var.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value + '&cont=1';
			}
			if(el.mode[i].value==3)
			{
				s='g1_scrmkr.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value;
			}
		}
	}
	document.location.href=s;
}

function tt2()
{
	var el = document.forms['f1'];
	var iCnt = 0;var s;
	for (var i=0;i<el.mode.length;i++)
	{
		if (el.mode[i].checked)
		{
			if(el.mode[i].value==1)
			{
				s='g1_var.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value + '&d=' + el.d.options[el.d.selectedIndex].value + '&r=' + el.r.options[el.r.selectedIndex].value;
			}
			if(el.mode[i].value==2)
			{
				s='g1_var.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value + '&cont=1';
			}
			if(el.mode[i].value==3)
			{
				s='g1_scrmkr.aspx?ds=' + el.ds.options[el.ds.selectedIndex].value;
			}
		}
	}
	document.location.href=s;
}

function cmm(e)
{
	var el = document.forms['f1'];
	var iCnt = 0;var s;
	s=e + el.ds.options[el.ds.selectedIndex].value;
	document.location.href=s;
}
function cmmz(e,ds)
{
	var iCnt = 0;var s;
	s=e + ds.options[ds.selectedIndex].value;
	document.location.href=s;
}

