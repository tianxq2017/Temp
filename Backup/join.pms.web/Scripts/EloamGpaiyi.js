var dev1;
var dev2;
var video1;
var video2;

function plugin()
{
    return document.getElementById('GpyView');
}

function GpyView()
{
    return document.getElementById('GpyView');
}


function thumb1()
{
    return document.getElementById('thumb1');
}

function addEvent(obj, name, func)
{
    if (obj.attachEvent) {
        obj.attachEvent("on"+name, func);
    } else {
        obj.addEventListener(name, func, false); 
    }
}
/*
var select1 = document.getElementById('selRes1');
var nResolution1 = select1.selectedIndex;
0,2592*1944
1,
3,1280*1024
4,1280*960
5,1024*768
6,800*600
7,640*480
8,320*240
*/
function OpenVideo()
{	
    try 
    { 
    	CloseVideo();
	
	    var subtype1 = (plugin().Device_GetSubtype(dev1) == 2?2:1);//1 表示仅支持YUY2 ，2 表示仅支持MJPG，3 表示两者均支持
	    var select1 = document.getElementById('selRes1');
        var nResolution1 = select1.selectedIndex;
	    //alert(nResolution1);
    						
	    video1 = plugin().Device_CreateVideo(dev1, nResolution1, subtype1);
	    if (video1)
	    {
		    GpyView().View_SelectVideo(video1);
		    GpyView().View_SetText("打开视频中，请等待...", 0);								
	    }
    } 
    catch (e) 
    { 
        alert(e.message); 
        CloseVideo();
    } 
}

function CloseVideo()
{
	if (video1)
	{
		GpyView().View_SetText("", 0);
		plugin().Video_Release(video1);
		video1 = null;
	}		
}

function Load()
{
	//设备接入和丢失
	//type设备类型， 1 表示视频设备， 2 表示音频设备
	//idx设备索引
	//dbt 1 表示设备到达， 2 表示设备丢失		
	addEvent(plugin(), 'DevChange', function(type,idx,dbt)
	{
		if (1 == type)
		{
			if (0 == idx)
			{
				if (1 == dbt)
				{
					dev1 = plugin().Global_CreateDevice(1, 0);
					if (dev1)
					{
						var subtype = (plugin().Device_GetSubtype(dev1) == 2?2:1);//1 表示仅支持YUY2 ，2 表示仅支持MJPG，3 表示两者均支持							
						video1 = plugin().Device_CreateVideo(dev1, 6, subtype);
						if (video1)
						{
							GpyView().View_SelectVideo(video1);
							GpyView().View_SetText("打开视频中，请等待...", 0);								
						}
						
						var select = document.getElementById('selRes1'); 	
						var nResolution = plugin().Device_GetResolutionCount(dev1);
						for(var i = 0; i < nResolution; i++)
						{
							var width = plugin().Device_GetResolutionWidth(dev1, i);
							var heigth = plugin().Device_GetResolutionHeight(dev1, i);
							select.add(new Option(width.toString() + "*" + heigth.toString())); 
						}
					}						
				}
				else if (2 == dbt)
				{
					if (video1)
					{
						GpyView().View_SetText("", 0);
						plugin().Video_Release(video1);
						video1 = null;
						plugin().Device_Release(dev1);
						dev1 = null;
						
						document.getElementById('selRes1').options.length = 0; 
					}
				}						
			}
			else if (1 == idx)
			{
				if (1 == dbt)
				{
					
				}
				else if (2 == dbt)
				{
					if (video1)
					{
						GpyView().View_SetText("", 0);
						plugin().Video_Release(video1);
						video1 = null;
						plugin().Device_Release(dev1);
						dev1 = null;
						
						document.getElementById('selRes1').options.length = 0; 
					}
				}
			}		
		}
    });

	addEvent(plugin(), 'Ocr', function(flag, ret)
	{
		if (1 == flag && 0 == ret)
		{
			var ret = plugin().Global_GetOcrPlainText(0);
			alert(ret);
		}
	});
	
	addEvent(plugin(), 'IdCard', function(ret)
	{
		if (1 == ret)
		{
			var str = "";
			str += plugin().Global_GetIdCardData(1);
			str += ";";
			str += plugin().Global_GetIdCardData(2);
			str += ";";
			str += plugin().Global_GetIdCardData(3);
			str += ";";		
			str += plugin().Global_GetIdCardData(4);
			str += "年";		
			str += plugin().Global_GetIdCardData(5);
			str += "月";		
			str += plugin().Global_GetIdCardData(6);
			str += "日";
			str += ";";			
			str += plugin().Global_GetIdCardData(7);
			str += ";";	
			str += plugin().Global_GetIdCardData(8);
			str += ";";	
			str += plugin().Global_GetIdCardData(9);
			str += ";";	
			str += plugin().Global_GetIdCardData(10);
			str += "年";		
			str += plugin().Global_GetIdCardData(11);
			str += "月";		
			str += plugin().Global_GetIdCardData(12);
			str += "日";
			str += "-";					
			str += plugin().Global_GetIdCardData(13);
			str += "年";		
			str += plugin().Global_GetIdCardData(14);
			str += "月";		
			str += plugin().Global_GetIdCardData(15);
			str += "日";	
		
			document.getElementById("idcard").value=str;	
		}
	});
	
	addEvent(plugin(), 'Biokey', function(ret)
	{
		if (4 == ret)
		{
			// 采集模板成功
			var mem = plugin().Global_GetBiokeyTemplateData();
			if (mem)
			{
				if (plugin().Memory_Save(mem, "D:\\1.tmp"))
				{
					document.getElementById("biokey").value="获取模板成功，存储路径为D:\\1.tmp";
				}						
				plugin().Memory_Release(mem);
			}
			
			var img = plugin().Global_GetBiokeyImage();
			plugin().Image_Save(img, "D:\\BiokeyImg1.jpg", 0);

			document.getElementById("BiokeyImg1").src= "D:\\BiokeyImg1.jpg";
			img.Destroy();
		}
		else if (8 == ret)
		{
			var mem = plugin().Global_GetBiokeyFeatureData();
			if (mem)
			{
				if (plugin().Memory_Save(mem, "D:\\2.tmp"))
				{
					document.getElementById("biokey").value="获取特征成功，存储路径为D:\\2.tmp";
				}						
				plugin().Memory_Release(mem);
			}
			
			var img = plugin().Global_GetBiokeyImage();
			plugin().Image_Save(img, "D:\\BiokeyImg2.jpg", 0);
			document.getElementById("BiokeyImg2").src= "D:\\BiokeyImg2.jpg";
			img.Destroy();
		}
	});
	
	addEvent(plugin(), 'Reader', function(type, subtype)
	{
		var str = "";
		if (4 == type)
		{
			str += "[CPU卡] Id:";
			str += plugin().Global_ReaderGetCpuId();
			str += "; CardNumber:";
			str += plugin().Global_ReaderGetCpuCreditCardNumber();
		}
		else if (2 == type)
		{
			str += "[M1卡] Id:";
			str += plugin().Global_ReaderGetM1Id();
		}
		else if (3 == type)
		{
			str += "[Memory卡] Id:";
			str += plugin().Global_ReaderGetMemoryId();	
		}
		
		document.getElementById("reader").value=str;
	});
	
	addEvent(plugin(), 'Mag', function(ret)
	{
		var str = "";
		
		str += "[磁卡] 卡号:";
		str += plugin().Global_MagneticCardGetNumber();
		
		document.getElementById("mag").value=str;
	});
	
	addEvent(plugin(), 'MoveDetec', function(video, id)
	{
		// 自动拍照事件	
	});
	
	addEvent(plugin(), 'Deskew', function(video, view, list)
	{
		// 纠偏回调事件
		var count = plugin().RegionList_GetCount(list);				
		for (var i = 0; i < count; ++i)
		{				
			var region = plugin().RegionList_GetRegion(list, i);
			
			var x1 = plugin().Region_GetX1(region);
			var y1 = plugin().Region_GetY1(region);
			
			var width = plugin().Region_GetWidth(region);
			var height = plugin().Region_GetHeight(region);
			
			plugin().Region_Release(region);
		}
		
		plugin().RegionList_Release(list);
	});
		
	GpyView().Global_SetWindowName("view");
	thumb1().Global_SetWindowName("thumb");

	plugin().Global_InitDevs();	
	plugin().Global_InitOcr();
	plugin().Global_InitIdCard();
	plugin().Global_InitBiokey();
	plugin().Global_InitReader();
	plugin().Global_InitMagneticCard();
	
	plugin().Global_DiscernIdCard();
	plugin().Global_ReaderStart();
	plugin().Global_MagneticCardReaderStart();
}

function Unload()
{
	if (video1)
	{
		GpyView().View_SetText("", 0);
		plugin().Video_Release(video1);
		video1 = null;
		plugin().Device_Release(dev1);
		dev1 = null;
	}
	
	plugin().Global_DeinitMagneticCard();
	plugin().Global_DeinitReader();
	plugin().Global_DeinitBiokey();
	plugin().Global_DeinitIdCard();
	plugin().Global_DeinitOcr();
	plugin().Global_DeinitDevs();
}


function AddText(obj)
{
	if (obj.checked)
	{			
		var font;
		font = plugin().Global_CreateTypeface(100, 100, 200, 0, 2, 0, 0, 0, "宋体");
		
		if (video1)
		{
			var width = plugin().Video_GetWidth(video1);
			var heigth = plugin().Video_GetHeight(video1);
			
			plugin().Video_EnableAddText(video1, font, width/2, heigth/2, "通辽市卫计局在线政务管理系统", 0x0000FF, 150);
		}
		plugin().Font_Release(font);
	}
	else
	{
		plugin().Video_DisableAddText(video1);
	}		
}


//拍照
function Scan()
{
     // var img = plugin().Video_CreateImage(video1, 0, GpyView().View_GetObject());
     //alert(document.getElementById('selRes1').selectedIndex +"-" +document.getElementById('selRes1').value);
	var imgList = plugin().Video_CreateImageList(video1, 0, GpyView().View_GetObject());
	if (imgList)
	{
		var len = plugin().ImageList_GetCount(imgList);
		for (var i = 0; i < len; i++)
		{
			var img = plugin().ImageList_GetImage(imgList, i);
			
			var date = new Date();
			var yy = date.getFullYear().toString();
			var mm = (date.getMonth() + 1).toString();
			var dd = date.getDate().toString();
			var hh = date.getHours().toString();
			var nn = date.getMinutes().toString();
			var ss = date.getSeconds().toString();
			var mi = date.getMilliseconds().toString();
			var Name = "D:\\" + yy + mm + dd + hh + nn + ss + mi + ".jpg";
		
			var b = plugin().Image_Save(img, Name, 0);
			if (b)
			{
				GpyView().View_PlayCaptureEffect();
				thumb1().Thumbnail_Add(Name);
			}
			
			plugin().Image_Release(img);
		}
		
		plugin().ImageList_Release(imgList);
	}
}
//单张拍照扫描保存到本地后上传 txtLocalScanFile
function ScanToUpload()
{
    document.getElementById("ButUploadFile").disabled=true;//设置提交按钮不可用
	var img = plugin().Video_CreateImage(video1, 0x10, GpyView().View_GetObject());
	if (img)
	{
        var date = new Date();
		var yy = date.getFullYear().toString();
		var mm = (date.getMonth() + 1).toString();
		var dd = date.getDate().toString();
		var hh = date.getHours().toString();
		var nn = date.getMinutes().toString();
		var ss = date.getSeconds().toString();
		var mi = date.getMilliseconds().toString();
		var locPath = "C:\\Temp\\";
		var Name = yy + mm + dd + hh + nn + ss + mi + ".jpg";
	
	    // 扫描拍照后保存到本地 ButUploadFile
		var b = plugin().Image_Save(img, locPath + Name, 0);
		if (b)
		{
		    document.getElementById("txtTmpUpFiles").value=Name;
		    //alert(document.getElementById("txtUploadFiles").value);
			GpyView().View_PlayCaptureEffect();
			plugin().Image_Release(img);
			//close video 
			CloseVideo();
			//upload
			ScanToHttpServerNow(locPath,Name);
			//set return value
			GetFilesInfo();
			
			document.getElementById("ButUploadFile").disabled=false;
			return true;
		}
		else
		{
		    plugin().Image_Release(img);
		    document.getElementById("ButUploadFile").disabled=false;
		    return false;
		}
	}
	else{
	    document.getElementById("ButUploadFile").disabled=false;
	    return false;
	}
	//释放相关资源
	Unload();
}

function ScanToHttpServerNow(objPath,objFile)
{
	//var http = plugin().Global_CreateHttp("http://localhost:15524/userctrl/WebForm1.aspx");
	var http = plugin().Global_CreateHttp("http://222.74.115.226:8084/Files/upload.asp");
	if (http)
	{
		var b = plugin().Http_UploadImageFile(http, objPath + objFile, objFile);
		if (b)
		{
			//alert("上传成功");
			document.getElementById("LabelMsg").innerText = "扫描文件上传成功！";
		}
		else
		{
			//alert("上传失败");
			document.getElementById("LabelMsg").innerText = "扫描文件上传失败！";
		}
		
		plugin().Http_Release(http);
	}
	else
	{
		alert("url 错误");
	}
}


function Ocr()
{
	var img = plugin().Video_CreateImage(video1, 0, GpyView().View_GetObject());
	if (img)
	{
		plugin().Global_DiscernOcr(1, img);
		plugin().Image_Release(img);
	}		
}

//本地上传 LabelMsg
function UploadToHttpServer()
{
	var http = plugin().Global_CreateHttp("http://localhost:15524/userctrl/WebForm1.aspx");
	if (http)
	{
		var b = plugin().Http_UploadImageFile(http, "D:\\201512516246989.jpg", "2.jpg");
		if (b)
		{
			alert("上传成功");
		}
		else
		{
			alert("上传失败");
		}
		
		plugin().Http_Release(http);
	}
	else
	{
		alert("url 错误");
	}
}
//直接拍照上传
function ScanToHttpServer()
{
	if(video1)
	{
		var img = plugin().Video_CreateImage(video1, 0x10, GpyView().View_GetObject());
		if (img)
		{
			//var http = plugin().Global_CreateHttp("http://192.168.1.193:8080/upload.asp");//asp服务器demo地址
			//var http = plugin().Global_CreateHttp("http://192.168.1.205:8080/FileStreamDemo/servlet/FileSteamUpload?");//java服务器demo地址
			var http = plugin().Global_CreateHttp("http://localhost:15524/userctrl/WebForm1.aspx");
			if (http)
			{
				var b = plugin().Http_UploadImage(http, img, 2, 0, "2.jpg");
				if (b)
				{
					alert("上传成功");
				}
				else
				{
					alert("上传失败");
				}
				
				plugin().Http_Release(http);
			}

			plugin().Image_Release(img);
		}
	}
}

//条码/二维码识别
function DiscernBarcode()
{
	if (plugin().Global_InitBarcode())
	{
		var image;
		image = plugin().Global_CreateImageFromFile("D:\\barcode.jpg", 0);
		
		if(plugin().Global_DiscernBarcode(image))
		{
			var count = plugin().Global_GetBarcodeCount();
			for(var i = 0; i < count; i++)
			{
				var read = plugin().Global_GetBarcodeData(i);
				alert(read);
			}
			
			if(count == 0)
				alert("没有检测到条码！");																				
		}
		else
		{
			alert("条码识别失败！");			
		}
		plugin().Image_Release(image);
	}	
	else
	{
		alert("初始化条码识别失败！");			
	}
	
	plugin().Global_DeinitBarcode();
}



