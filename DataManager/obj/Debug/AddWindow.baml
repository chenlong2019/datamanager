   M S B A M L   `   `   `  ����F  BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager  5   	       DataManager.AddWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	   -   5          � Title$  添加��5   	   $	��480��6   $	��800��6!    � 
ResizeMode$ CanMinimize=�6-    � WindowStartupLocation$ CenterScreen=�6F   .��5	      � .+�5
   
   �� ���	#FFF4F4F5$��1��6&   $��	0,100,0,0q�61   
���   �$	��Top=�6S   �� 5   
   $��本地上传��6   $
��Left=�6   $��5,5,0,0q�6:   $	��Top=�6K   $
��12px(�6c   �� 5   
   $��
数据包:��6   $
��Left=�6   $��	31,49,0,0q�6:   $	��Top=�6M   $��30��6e   $��44��6q   �� 5   
   -   5        $��txt_Data��5   ?   $
��Left=�6    �� 
IsReadOnly . $��30��6O   $��
123,49,0,0q�6[    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��409��6�   �� 5   
   -   5        $��	选择...��5      $
��Left=�6>   $��
573,49,0,0q�6Y   $	��Top=�6m   $��30��6�   $��68��6�   �� 5   
   $��详细信息：��6   $
��Left=�6    $��	5,105,0,0q�6;   $	��Top=�6N   �� 5   
   $��职员编号:��6   $
��Left=�6    $��
31,135,0,0q�6;   $	��Top=�6O   �� 5   
   $��入库人员:��6   $
��Left=�6    $��210,135,0,0q�6;   $	��Top=�6P   �� 5   
   $��卫星轨道:��6   $
��Left=�6    $��
31,170,0,0q�6;   $	��Top=�6O   �� 5   
   $��拍摄卫星:��6   $
��Left=�6    $��411,135,0,0q�6;   $	��Top=�6P   �� 5   
   $��拍摄日期:��6   $
��Left=�6    $��411,170,0,0q�6;   $	��Top=�6P   �� 5   
   -   5        $��txt_StaffNumber��5   -   $
��Left=�6   $��23��6D   $��
92,135,0,0q�6P   $
 Wrap=�6d   $	��Top=�6y   $	��109��6�   �� 5   
   -   5        $��
txt_People��5   -   $
��Left=�6   $��23��6?   $��272,135,0,0q�6K   $
 Wrap=�6`   $	��Top=�6u   $	��109��6�   �� 5   
   -   5        $��	cmb_Orbit��5   .   $
��Left=�6   $��
92,170,0,0q�6?   $	��Top=�6S   $	��289��6k   �� 5   
   -   5        $��cmb_Satellite��5   /   $
��Left=�6   $��472,135,0,0q�6D   $	��Top=�6Y   $	��289��6q   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtp_Time��5   0   $
��Left=�6      Text$  ��6@   $��472,170,0,0q�6H   $	��Top=�6]   $	��145��6u   �� 5   
   -	   5        $��上传��5      $
��Left=�67   $��168,363,0,0q�6R   $	��Top=�6g   $��60��6   $��30��6�   �� 5   
   -
   5        $��取消��5      $
��Left=�66   $��443,363,0,0q�6Q   $	��Top=�6f   $��60��6~   $��30��6�   5      5      