﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsレーン太鼓 : CActivity
    {
        /// <summary>
        /// レーンを描画するクラス。
        /// 
        /// 
        /// </summary>
        public CAct演奏Drumsレーン太鼓()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            for( int i = 0; i < 2; i++ )
			{
				this.st状態[ i ].ct進行 = new CCounter();
			}
            this.ctゴーゴー = new CCounter();
            this.ct分岐アニメ進行 = new CCounter();
            this.nフラッシュ制御タイマ = -1;
            this.nBranchレイヤー透明度 = 0;
            this.nBranch文字透明度 = 0;
            this.nY座標 = 0;
            this.n総移動時間  = -1;
            this.nDefaultJudgePos[ 0 ] = CDTXMania.Skin.nScrollFieldX;
            this.nDefaultJudgePos[ 1 ] = CDTXMania.Skin.nScrollFieldY;
            this.ctゴーゴー炎 = new CCounter( 0, 6, 50, CDTXMania.Timer );
            base.On活性化();
        }

        public override void On非活性化()
        {
            for( int i = 0; i < 2; i++ )
			{
				this.st状態[ i ].ct進行 = null;
			}
            CDTXMania.Skin.nScrollFieldX = this.nDefaultJudgePos[ 0 ];
            CDTXMania.Skin.nScrollFieldY = this.nDefaultJudgePos[ 1 ];
            this.ctゴーゴー = null;
            this.ct分岐アニメ進行 = null;
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            this.txLane = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_fieldbgA.png" ) );
            this.txLaneB = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_fieldbgB.png" ) );
            this.txゴーゴー = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_fieldbgC.png" ) );
            this.tx普通譜面[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_normal_base.png" ) );
            this.tx玄人譜面[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_expert_base.png" ) );
            this.tx達人譜面[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_master_base.png" ) );
            this.tx普通譜面[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_normal.png" ) );
            this.tx玄人譜面[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_expert.png" ) );
            this.tx達人譜面[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_field_master.png" ) );
            this.tx枠線 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_B.png" ) );
            this.tx判定枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_taiko_notes.png" ) );
            //this.txアタックエフェクトLower = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_explosion_lower.png" ) );

            this.txゴーゴー炎 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gogo_fire.png" ) );

            this.txArアタックエフェクトLower_A = new CTexture[ 15 ];
            this.txArアタックエフェクトLower_B = new CTexture[ 15 ];
            this.txArアタックエフェクトLower_C = new CTexture[ 15 ];
            this.txArアタックエフェクトLower_D = new CTexture[ 15 ];
            for( int i = 0; i < 15; i++ )
            {
                this.txArアタックエフェクトLower_A[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Effect\lower_1_" + i.ToString() + ".png" ) );
                this.txArアタックエフェクトLower_B[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Effect\lower_2_" + i.ToString() + ".png" ) );
                this.txArアタックエフェクトLower_C[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Effect\lower_3_" + i.ToString() + ".png" ) );
                this.txArアタックエフェクトLower_D[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Effect\lower_4_" + i.ToString() + ".png" ) );

                //this.txArアタックエフェクトLower_A[ i ].b加算合成 = true;
                //this.txArアタックエフェクトLower_B[ i ].b加算合成 = true;
                //this.txArアタックエフェクトLower_C[ i ].b加算合成 = true;
                //this.txArアタックエフェクトLower_D[ i ].b加算合成 = true;
            }
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txLane );
            CDTXMania.tテクスチャの解放( ref this.txLaneB );
            CDTXMania.tテクスチャの解放( ref this.txゴーゴー );
            CDTXMania.tテクスチャの解放( ref this.tx普通譜面[ 0 ] );
            CDTXMania.tテクスチャの解放( ref this.tx玄人譜面[ 0 ] );
            CDTXMania.tテクスチャの解放( ref this.tx達人譜面[ 0 ] );
            CDTXMania.tテクスチャの解放( ref this.tx普通譜面[ 1 ] );
            CDTXMania.tテクスチャの解放( ref this.tx玄人譜面[ 1 ] );
            CDTXMania.tテクスチャの解放( ref this.tx達人譜面[ 1 ] );

            CDTXMania.tテクスチャの解放( ref this.tx枠線 );
            CDTXMania.tテクスチャの解放( ref this.tx判定枠 );
            //CDTXMania.tテクスチャの解放( ref this.txアタックエフェクトLower );

            CDTXMania.tテクスチャの解放( ref this.txゴーゴー炎 );

            for( int i = 0; i < 15; i++ )
            {
                CDTXMania.tテクスチャの解放( ref this.txArアタックエフェクトLower_A[ i ] );
                CDTXMania.tテクスチャの解放( ref this.txArアタックエフェクトLower_B[ i ] );
                CDTXMania.tテクスチャの解放( ref this.txArアタックエフェクトLower_C[ i ] );
                CDTXMania.tテクスチャの解放( ref this.txArアタックエフェクトLower_D[ i ] );
            }

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( base.b初めての進行描画 )
            {
                this.nフラッシュ制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
                base.b初めての進行描画 = false;
            }

            long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
            if( num < this.nフラッシュ制御タイマ )
            {
                this.nフラッシュ制御タイマ = num;
            }
            while( ( num - this.nフラッシュ制御タイマ ) >= 30 )
            {
                if( this.nBranchレイヤー透明度 <= 255 )
                {
                    this.nBranchレイヤー透明度 += 10;
                }

                if( this.nBranch文字透明度 >= 0 )
                {
                    this.nBranch文字透明度 -= 10;
                }

                if( this.nY座標 != 0 && this.nY座標 <= 20 )
                {
                    this.nY座標++;
                }

                this.nフラッシュ制御タイマ += 8;
            }

            if( !this.ct分岐アニメ進行.b停止中 )
            {
                this.ct分岐アニメ進行.t進行();
                if( this.ct分岐アニメ進行.b終了値に達した )
                {
                    this.ct分岐アニメ進行.t停止();
                }
            }

            #region[ レーンタイプA ]
            if( this.txLane != null )
            {
                this.txLane.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY );

                if( CDTXMania.stage演奏ドラム画面.bDoublePlay )
                    this.txLane.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY + 176 );
            }


            if( CDTXMania.stage演奏ドラム画面.bUseBranch == true )
            {
                #region[ 動いていない ]
                switch( CDTXMania.stage演奏ドラム画面.n次回のコース )
                {
                    case 0:
                        if( this.tx普通譜面[ 0 ] != null )
                            this.tx普通譜面[ 0 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY );
                        break;
                    case 1:
                        if( this.tx玄人譜面[ 0 ] != null )
                            this.tx玄人譜面[ 0 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY );
                        break;
                    case 2:
                        if( this.tx達人譜面[ 0 ] != null )
                            this.tx達人譜面[ 0 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY );
                        break;
                }
                #endregion

                if( CDTXMania.ConfigIni.nBranchAnime == 1 )
                {
                    #region[ AC7～14風の背後レイヤー ]
                    if( this.ct分岐アニメ進行.b進行中 )
                    {
                        int n透明度 = ( ( 100 - this.ct分岐アニメ進行.n現在の値 ) * 0xff ) / 100;

                        if( this.ct分岐アニメ進行.b終了値に達した )
                        {
                            n透明度 = 255;
                            this.ct分岐アニメ進行.t停止();
                        }

                        #region[ 普通譜面・レベルアップ ]
                        //普通→玄人
                        if( n1 == 0 && n2 == 1 )
                        {
                            if( this.tx普通譜面[ 0 ] != null && this.tx玄人譜面[ 0 ] != null )
                            {
                                this.tx普通譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx玄人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx玄人譜面[ 0 ].n透明度 = this.nBranchレイヤー透明度;
                            }
                        }
                        //普通→達人
                        if( n1 == 0 && n2 == 2 )
                        {
                            if( this.ct分岐アニメ進行.n現在の値 < 100 )
                            {
                                n透明度 = ( ( 100 - this.ct分岐アニメ進行.n現在の値 ) * 0xff ) / 100;
                            }
                            if( this.tx普通譜面[ 0 ] != null && this.tx達人譜面[ 0 ] != null )
                            {
                                this.tx普通譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx達人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx達人譜面[ 0 ].n透明度 = this.nBranchレイヤー透明度;
                            }
                        }
                        #endregion
                        #region[ 玄人譜面・レベルアップ ]
                        if( n1 == 1 && n2 == 2 )
                        {
                            if( this.tx玄人譜面[ 0 ] != null && this.tx達人譜面[ 0 ] != null )
                            {
                                this.tx玄人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx達人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx達人譜面[ 0 ].n透明度 = this.nBranchレイヤー透明度;
                            }
                        }
                        #endregion
                        #region[ 玄人譜面・レベルダウン ]
                        if( n1 == 1 && n2 == 0 )
                        {
                            if( this.tx玄人譜面[ 0 ] != null && this.tx普通譜面[ 0 ] != null )
                            {
                                this.tx玄人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx普通譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx普通譜面[ 0 ].n透明度 = this.nBranchレイヤー透明度;
                            }
                        }
                        #endregion
                        #region[ 達人譜面・レベルダウン ]
                        if( n1 == 2 && n2 == 0 )
                        {
                            if( this.tx達人譜面[ 0 ] != null && this.tx普通譜面[ 0 ] != null )
                            {
                                this.tx達人譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx普通譜面[ 0 ].t2D描画( CDTXMania.app.Device, 333, 192 );
                                this.tx普通譜面[ 0 ].n透明度 = this.nBranchレイヤー透明度;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else if( CDTXMania.ConfigIni.nBranchAnime == 0 )
                {
                    CDTXMania.stage演奏ドラム画面.actLane.On進行描画();
                }
            }
            #region[ ゴーゴータイムレーン背景レイヤー ]
            if( this.txゴーゴー != null && CDTXMania.stage演奏ドラム画面.bIsGOGOTIME )
            {
                if( !this.ctゴーゴー.b停止中 )
                {
                    this.ctゴーゴー.t進行();
                }

                if( this.ctゴーゴー.n現在の値 <= 4 )
                {
                    this.txゴーゴー.vc拡大縮小倍率.Y = 0.2f;
                    this.txゴーゴー.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY + 54 );
                }
                else if( this.ctゴーゴー.n現在の値 <= 5 )
                {
                    this.txゴーゴー.vc拡大縮小倍率.Y = 0.4f;
                    this.txゴーゴー.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY + 40 );
                }
                else if( this.ctゴーゴー.n現在の値 <= 6 )
                {
                    this.txゴーゴー.vc拡大縮小倍率.Y = 0.6f;
                    this.txゴーゴー.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY + 26 );
                }
                else if( this.ctゴーゴー.n現在の値 <= 8 )
                {
                    this.txゴーゴー.vc拡大縮小倍率.Y = 0.8f;
                    this.txゴーゴー.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY + 13 );
                }
                else if( this.ctゴーゴー.n現在の値 >= 9 )
                {
                    this.txゴーゴー.vc拡大縮小倍率.Y = 1.0f;
                    this.txゴーゴー.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldBGX, CDTXMania.Skin.nScrollFieldBGY );
                }
            }
            #endregion


            if( CDTXMania.stage演奏ドラム画面.bUseBranch == true )
            {
                if (CDTXMania.ConfigIni.nBranchAnime == 0)
                {
                    if (!this.ct分岐アニメ進行.b進行中)
                    {

                        switch (CDTXMania.stage演奏ドラム画面.n次回のコース)
                        {
                            case 0:
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, CDTXMania.Skin.nScrollFieldBranchTextY);
                                break;
                            case 1:
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, CDTXMania.Skin.nScrollFieldBranchTextY);
                                break;
                            case 2:
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, CDTXMania.Skin.nScrollFieldBranchTextY);
                                break;
                        }
                    }

                    if (this.ct分岐アニメ進行.b進行中)
                    {

                        #region[ 普通譜面・レベルアップ ]
                        //普通→玄人
                        if (n1 == 0 && n2 == 1)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;

                            this.tx普通譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 60));
                            //this.tx玄人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 255 : ( ( ( this.ct分岐アニメ進行.n現在の値 * 0xff ) / 60 ) );
                            if (this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY);
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 162 + nY);
                            }
                            else
                            {
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }

                        }
                        
                        //普通→達人
                        if (n1 == 0 && n2 == 2)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;
                            if( this.ct分岐アニメ進行.n現在の値 < 5 )
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 180 + nY);
                                this.tx普通譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 100));
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 172 + nY);
                            }
                            if (this.ct分岐アニメ進行.n現在の値 >= 5 && this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY);
                                this.tx普通譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 100));
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 172 + nY);
                            }
                            else if (this.ct分岐アニメ進行.n現在の値 >= 60 && this.ct分岐アニメ進行.n現在の値 < 150)
                            {
                                this.nY = 21;
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                                this.tx玄人譜面[1].n透明度 = 255;
                                this.tx達人譜面[1].n透明度 = 255;
                            }
                            else if (this.ct分岐アニメ進行.n現在の値 >= 150 && this.ct分岐アニメ進行.n現在の値 < 210)
                            {
                                this.nY = ((this.ct分岐アニメ進行.n現在の値 - 150) / 2);
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY);
                                this.tx玄人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 100));
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 172 + nY);
                            }
                            else
                            {
                                this.tx達人譜面[1].n透明度 = 255;
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }


                            //this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY);
                            //this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 172 + nY);

                        }
                        #endregion
                        #region[ 玄人譜面・レベルアップ ]
                        //玄人→達人
                        if (n1 == 1 && n2 == 2)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;

                            this.tx玄人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 60));
                            if (this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY);
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 162 + nY);
                            }
                            else
                            {
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }


                        }
                        #endregion
                        #region[ 玄人譜面・レベルダウン ]
                        if (n1 == 1 && n2 == 0)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;

                            this.tx玄人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 60));
                            if (this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY);
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 222 - nY);
                            }
                            else
                            {
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }


                        }
                        #endregion
                        #region[ 達人譜面・レベルダウン ]
                        if (n1 == 2 && n2 == 0)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;

                            if (this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx達人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 60));
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY);
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 222 - nY);
                            }
                            else if (this.ct分岐アニメ進行.n現在の値 >= 60 && this.ct分岐アニメ進行.n現在の値 < 150)
                            {
                                this.nY = 21;
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                                this.tx玄人譜面[1].n透明度 = 255;
                                this.tx達人譜面[1].n透明度 = 255;
                            }
                            else if (this.ct分岐アニメ進行.n現在の値 >= 150 && this.ct分岐アニメ進行.n現在の値 < 210)
                            {
                                this.nY = ((this.ct分岐アニメ進行.n現在の値 - 150) / 2);
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY);
                                this.tx玄人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 100));
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 222 - nY);
                            }
                            else if( this.ct分岐アニメ進行.n現在の値 >= 210 )
                            {
                                this.tx普通譜面[1].n透明度 = 255;
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }
                        }
                        if (n1 == 2 && n2 == 1)
                        {
                            this.tx普通譜面[1].n透明度 = 255;
                            this.tx玄人譜面[1].n透明度 = 255;
                            this.tx達人譜面[1].n透明度 = 255;

                            this.tx達人譜面[1].n透明度 = this.ct分岐アニメ進行.n現在の値 > 100 ? 0 : (255 - ((this.ct分岐アニメ進行.n現在の値 * 0xff) / 60));
                            if (this.ct分岐アニメ進行.n現在の値 < 60)
                            {
                                this.nY = this.ct分岐アニメ進行.n現在の値 / 2;
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY);
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 222 - nY);
                            }
                            else
                            {
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                            }

                        }
                        #endregion
                    }
                }
                else
                {
                    if (this.nY座標 == 21)
                    {
                        this.nY座標 = 0;
                    }

                    if (this.nY座標 == 0)
                    {
                        switch (CDTXMania.stage演奏ドラム画面.n次回のコース)
                        {
                            case 0:
                                this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                                break;
                            case 1:
                                this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                                break;
                            case 2:
                                this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192);
                                break;
                        }
                    }


                    if (this.nY座標 != 0)
                    {

                        #region[ 普通譜面・レベルアップ ]
                        //普通→玄人
                        if (n1 == 0 && n2 == 1)
                        {
                            this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY座標);
                            this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 212 - nY座標);
                            this.tx普通譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        //普通→達人
                        if (n1 == 0 && n2 == 2)
                        {
                            this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY座標);
                            this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 212 - nY座標);
                            this.tx普通譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        #endregion
                        #region[ 玄人譜面・レベルアップ ]
                        //玄人→達人
                        if (n1 == 1 && n2 == 2)
                        {
                            this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 - nY座標);
                            this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 212 - nY座標);
                            this.tx玄人譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        #endregion

                        #region[ 玄人譜面・レベルダウン ]
                        if (n1 == 1 && n2 == 0)
                        {
                            this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY座標);
                            this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 168 + nY座標);
                            this.tx玄人譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        #endregion
                        #region[ 達人譜面・レベルダウン ]
                        if (n1 == 2 && n2 == 0)
                        {
                            this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY座標);
                            this.tx普通譜面[1].t2D描画(CDTXMania.app.Device, 333, 168 + nY座標);
                            this.tx達人譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        if (n1 == 2 && n2 == 1)
                        {
                            this.tx達人譜面[1].t2D描画(CDTXMania.app.Device, 333, 192 + nY座標);
                            this.tx玄人譜面[1].t2D描画(CDTXMania.app.Device, 333, 168 + nY座標);
                            this.tx達人譜面[1].n透明度 = this.nBranchレイヤー透明度;
                        }
                        #endregion
                    }
                }

            }


            if( this.txLaneB != null )
            {
                this.txLaneB.t2D描画( CDTXMania.app.Device, 333, 326 );
                if( CDTXMania.stage演奏ドラム画面.bDoublePlay )
                {
                    this.txLaneB.t2D描画( CDTXMania.app.Device, 333, 502 );
                }
            }


            CDTXMania.stage演奏ドラム画面.actLaneFlushD.On進行描画();



            if( this.tx枠線 != null )
            {
                this.tx枠線.t2D描画( CDTXMania.app.Device, 329, 136, new Rectangle( 0, 0, 951, 224 ) );

                if( CDTXMania.stage演奏ドラム画面.bDoublePlay )
                {
                    this.tx枠線.t2D描画( CDTXMania.app.Device, 329, 360, new Rectangle( 0, 224, 951, 224 ) );
                }
            }

            if( CDTXMania.stage演奏ドラム画面.bIsGOGOTIME )
            {
                this.ctゴーゴー炎.t進行Loop();


                if( this.txゴーゴー炎 != null )
                {
                    float f倍率 = 1.0f;

                    float[] ar倍率 = new float[] { 0.8f, 1.2f, 1.7f, 2.5f, 2.3f, 2.2f, 2.0f, 1.8f, 1.7f, 1.6f, 1.6f, 1.5f, 1.5f, 1.4f, 1.3f, 1.2f, 1.1f, 1.0f };

                    f倍率 = ar倍率[ this.ctゴーゴー.n現在の値 ];

                    Matrix mat = Matrix.Identity;
                    mat *= Matrix.Scaling(f倍率, f倍率, 1.0f);
                    mat *= Matrix.Translation( CDTXMania.Skin.nScrollFieldX - SampleFramework.GameWindowSize.Width / 2.0f, -(257 - SampleFramework.GameWindowSize.Height / 2.0f), 0f);

                    this.txゴーゴー炎.b加算合成 = true;

                    //this.ctゴーゴー.n現在の値 = 6;
                    if( this.ctゴーゴー.b終了値に達した )
                    {
                        this.txゴーゴー炎.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldX - 180, 72, new Rectangle( 360 * ( this.ctゴーゴー炎.n現在の値 ), 0, 360, 370 ) );
                    }
                    else
                    {
                        this.txゴーゴー炎.t3D描画( CDTXMania.app.Device, mat, new Rectangle( 360 * ( this.ctゴーゴー炎.n現在の値 ), 0, 360, 370 ) );
                    }
                }
            }

            if( this.n総移動時間 != -1 )
            {
                if( n移動方向 == 1 )
                    CDTXMania.Skin.nScrollFieldX = this.n移動開始X + (int)( ( ( (int)CSound管理.rc演奏用タイマ.n現在時刻ms - this.n移動開始時刻 ) / (double)( this.n総移動時間 ) ) * this.n移動距離px );
                else
                    CDTXMania.Skin.nScrollFieldX = this.n移動開始X - (int)( ( ( (int)CSound管理.rc演奏用タイマ.n現在時刻ms - this.n移動開始時刻 ) / (double)( this.n総移動時間 ) ) * this.n移動距離px );

                if( ( (int)CSound管理.rc演奏用タイマ.n現在時刻ms ) > this.n移動開始時刻 + this.n総移動時間 )
                {
                    this.n総移動時間 = -1;
                }
            }


            if( this.tx判定枠 != null )
            {
                int nJudgeX = CDTXMania.Skin.nScrollFieldX - ( ( this.tx判定枠.sz画像サイズ.Width / 13 ) / 2 ); //元の値は349なんだけど...
                int nJudgeY = CDTXMania.Skin.nScrollFieldY; //元の値は349なんだけど...
                this.tx判定枠.b加算合成 = true;
                this.tx判定枠.t2D描画( CDTXMania.app.Device, nJudgeX, nJudgeY, new Rectangle( 0, 0, 130, 130 ) );

                if( CDTXMania.stage演奏ドラム画面.bDoublePlay )
                    this.tx判定枠.t2D描画( CDTXMania.app.Device, nJudgeX, nJudgeY + 176, new Rectangle( 0, 0, 130, 130 ) );
            }

            for( int i = 0; i < 1; i++ )
			{
				if( !this.st状態[ i ].ct進行.b停止中 )
				{
                    this.st状態[ i ].ct進行.t進行();
					if( this.st状態[ i ].ct進行.b終了値に達した )
					{
						this.st状態[ i ].ct進行.t停止();
					}
					//if( this.txアタックエフェクトLower != null )
					{
                        //this.txアタックエフェクトLower.b加算合成 = true;
                        int n = this.st状態[ i ].nIsBig == 1 ? 520 : 0;

                        switch( st状態[ i ].judge )
                        {
                            case E判定.Perfect:
                            case E判定.Great:
                            case E判定.Auto:
						        //this.txアタックエフェクトLower.t2D描画( CDTXMania.app.Device, 285, 127, new Rectangle( this.st状態[ i ].ct進行.n現在の値 * 260, n, 260, 260 ) );
                                if( this.st状態[ i ].nIsBig == 1 )
                                    this.txArアタックエフェクトLower_C[ this.st状態[ i ].ct進行.n現在の値 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldX - this.txArアタックエフェクトLower_C[0].szテクスチャサイズ.Width / 2, 127 );
                                else
                                    this.txArアタックエフェクトLower_A[ this.st状態[ i ].ct進行.n現在の値 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldX - this.txArアタックエフェクトLower_A[0].szテクスチャサイズ.Width / 2, 127 );
                                break;

                            case E判定.Good:
						        //this.txアタックエフェクトLower.t2D描画( CDTXMania.app.Device, 285, 127, new Rectangle( this.st状態[ i ].ct進行.n現在の値 * 260, n + 260, 260, 260 ) );
                                if( this.st状態[ i ].nIsBig == 1 )
                                    this.txArアタックエフェクトLower_D[ this.st状態[ i ].ct進行.n現在の値 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldX - this.txArアタックエフェクトLower_D[0].szテクスチャサイズ.Width / 2, 127 );
                                else
                                    this.txArアタックエフェクトLower_B[ this.st状態[ i ].ct進行.n現在の値 ].t2D描画( CDTXMania.app.Device, CDTXMania.Skin.nScrollFieldX - this.txArアタックエフェクトLower_B[0].szテクスチャサイズ.Width / 2, 127 );
                                break;

                            case E判定.Miss:
                            case E判定.Bad:
                                break;
                        }
					}
				}
            }

            #endregion

            if( CDTXMania.ConfigIni.bAVI有効 )
            {
                this.txLane.n透明度 = 240;
                this.txLaneB.n透明度 = 240;
                this.txゴーゴー.n透明度 = 240;
            }

            //CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, this.nBranchレイヤー透明度.ToString());
            //CDTXMania.act文字コンソール.tPrint(0, 16, C文字コンソール.Eフォント種別.白, this.ct分岐アニメ進行.n現在の値.ToString());
            //CDTXMania.act文字コンソール.tPrint(0, 32, C文字コンソール.Eフォント種別.白, this.ct分岐アニメ進行.n終了値.ToString());

            return base.On進行描画();
        }


        public virtual void Start( int nLane, E判定 judge, bool b両手入力 )
		{
			for( int n = 0; n < 1; n++ )
			{
				this.st状態[ n ].ct進行 = new CCounter( 0, 14, 10, CDTXMania.Timer );
				this.st状態[ n ].judge = judge;

                switch( nLane )
                {
                    case 0x11:
                    case 0x12:
                        this.st状態[ n ].nIsBig = 0;
                        break;
                    case 0x13:
                    case 0x14:
                        {
                            if( b両手入力 )
                                this.st状態[ n ].nIsBig = 1;
                            else
                                this.st状態[ n ].nIsBig = 0;
                        }
                        break;
                }
			}
		}

        public void GOGOSTART()
        {
            this.ctゴーゴー = new CCounter( 0, 17, 18, CDTXMania.Timer );
            //if( this.ctゴーゴー.b停止中 )
                //this.ctゴーゴー.t進行();
        }


        public void t分岐レイヤー・コース変化( int n現在, int n次回 )
        {
            if( n現在 == n次回 )
            {
                return;
            }
            this.ct分岐アニメ進行 = new CCounter(0, 300, 2, CDTXMania.Timer);

            this.nBranchレイヤー透明度 = 6;
            this.nY座標 = 1;

            this.n1 = n現在;
            this.n2 = n次回;

            CDTXMania.stage演奏ドラム画面.actLane.t分岐レイヤー・コース変化( n現在, n次回 );
        }

        public void t判定枠移動( double db移動時間, int n移動px, int n移動方向 )
        {
            this.n移動開始時刻 = (int)CSound管理.rc演奏用タイマ.n現在時刻ms;
            this.n移動開始X = CDTXMania.Skin.nScrollFieldX;
            this.n総移動時間 = (int)( db移動時間 * 1000 );
            this.n移動方向 = n移動方向;
            this.n移動距離px = n移動px;
        }


        #region[ private ]
        //-----------------
        private CTexture txLane;
        private CTexture txLaneB;
        private CTexture tx枠線;
        private CTexture tx判定枠;
        private CTexture txゴーゴー;
        private CTexture txゴーゴー炎;
        private CTexture[] txArゴーゴー炎;
        private CTexture[] txArアタックエフェクトLower_A;
        private CTexture[] txArアタックエフェクトLower_B;
        private CTexture[] txArアタックエフェクトLower_C;
        private CTexture[] txArアタックエフェクトLower_D;

        private CTexture[] txLaneFlush = new CTexture[3];

        private CTexture[] tx普通譜面 = new CTexture[2];
        private CTexture[] tx玄人譜面 = new CTexture[2];
        private CTexture[] tx達人譜面 = new CTexture[2];

        private CTextureAf txアタックエフェクトLower;

        protected STSTATUS[] st状態 = new STSTATUS[2];

        [StructLayout(LayoutKind.Sequential)]
        protected struct STSTATUS
        {
            public CCounter ct進行;
            public E判定 judge;
            public int nIsBig;
            public int n透明度;
        }
        private CCounter ctゴーゴー;
        private CCounter ctゴーゴー炎;

        private int n1;
        private int n2;

        private long nフラッシュ制御タイマ;
        private int nBranchレイヤー透明度;
        private int nBranch文字透明度;
        private int nY座標;
        private int nY;

        private int n総移動時間;
        private int n移動開始X;
        private int n移動開始Y;
        private int n移動開始時刻;
        private int n移動距離px;
        private int n移動方向;

        private int[] nDefaultJudgePos = new int[ 2 ];

        public CCounter ct分岐アニメ進行;
        //-----------------
        #endregion
    }
}
