   M S B A M L   `   `   `  ����F  BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager  5   	       DataManager.LoadWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	   -   5          � Title$  	下载器��5   	   $	��200��6   $	��600��6"    � 
ResizeMode$ CanMinimize=�6.   .��5	      � .+�5
   
   � -   5        $��progressBar1��5
      $
��Left=�6*   $��30��6E   $��	80,35,0,0q�6Q   $	��Top=�6d   $	��501��6|   �� 5   
   -   5        $��lb_Speed��5       $��Label��6   $
��Left=�60   $��	80,70,0,0q�6K   $	��Top=�6^    J� RenderTransformOrigin$ 0.974,0.407&�6v   �� 5   
   -   5        $��	btn_Close��5      $��取消��6   $��Right=�6H   $��0,115,7.5,0q�6d   $	��Top=�6y   $��75��6�   $��30��6�   $��12(�6�   �� 5   
   $��下载进度:��6   $
��Left=�6    $��	15,35,0,0q�6;   $	��Top=�6N   �� 5   
   -   5        $��lb_FileName��5      $��
文件名:��6   $
��Left=�62   $��	10,10,0,0q�6M   $	��Top=�6`   5      5      