<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method='html' version='1.0' doctype-public='-//W3C//DTD HTML 4.01 Transitional//EN'
              doctype-system='http://www.w3.org/TR/html4/loose.dtd' encoding='UTF-8' indent='yes'/>

  <!-- Main Template -->
  <xsl:template match="/">
    <html>
      <head>
        <meta http-equiv="Content-Language" content="en-us" />
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
        <link rel="stylesheet" type="text/css" href="XmlDocComments.css" />
        <title>
          <xsl:value-of select="library/@name"/>
        </title>
      </head>
      <body>
        <div id="Main">
          <xsl:call-template name="library">
          </xsl:call-template>
        </div>
      </body>
    </html>
  </xsl:template>
  
  <!-- Library Template -->
  <xsl:template name="library">
    <h1>
      <xsl:value-of select="/library/@name"/>
    </h1>
    <div id="#classes" class="classes">
      <xsl:for-each select="/library/classes">
        <xsl:for-each select="./class">
          <xsl:call-template name="class">
          </xsl:call-template>
        </xsl:for-each>
      </xsl:for-each>
    </div>
  </xsl:template>

  <!-- Class Template -->
  <xsl:template name="class">
    <div class="class">
      <h2>
        <xsl:value-of select="./@name"/>
        <xsl:if test="@type">
          (<xsl:value-of select="./@type"/>)
        </xsl:if>
      </h2>
      <p>
        <xsl:value-of select="./@description"/>
      </p>
      <div class="functions">
        <xsl:for-each select="./function">
          <xsl:call-template name="function">
          </xsl:call-template>
        </xsl:for-each>
      </div>
    </div>
  </xsl:template>

  <!-- Function Template -->
  <xsl:template name="function">
    <div class="function">
      <h3>
        <xsl:value-of select="./@name"/>
        <xsl:if test="./@isConstructor">
          [Constructor]
        </xsl:if>
        <xsl:if test="./@group">
          (<xsl:value-of select="./@group"/>)
        </xsl:if>
      </h3>
      <div class="summary">
        <xsl:value-of select="./summary"/>
      </div>
      <xsl:if test="./param">
        <div class="params">
          <h4>Parameters</h4>
          <ul>
            <xsl:for-each select="./param">
              <li>
                <xsl:value-of select="./@name"/>
                <xsl:if test="@type">
                  : <xsl:value-of select="./@type"/>
                </xsl:if>
              </li>
            </xsl:for-each>
          </ul>
        </div>
      </xsl:if>
      <xsl:if test="./returns">
        <div class="returns">
          <h4>Returns</h4>
          <ul>
            <li>
              <xsl:value-of select="returns/@type"/>
            </li>
          </ul>
        </div>
      </xsl:if>
    </div>
  </xsl:template>

</xsl:stylesheet>
